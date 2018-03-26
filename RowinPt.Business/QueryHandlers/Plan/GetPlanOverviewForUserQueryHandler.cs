using AlperAslanApps.Core;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Plan;
using RowinPt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.CourseTypes
{
    internal sealed class GetPlanOverviewForUserQueryHandler : IQueryHandler<GetPlanOverviewForUserQuery, PlanOverview>
    {
        private readonly IReader<SubscriptionModel> _subscriptionReader;
        private readonly IReader<LocationModel> _locationReader;

        private readonly DateTime _min;
        private readonly DateTime _max;

        public GetPlanOverviewForUserQueryHandler(
            IReader<SubscriptionModel> subscriptionReader,
            IReader<LocationModel> locationReader,
            ITimeProvider timeProvider)
        {
            _subscriptionReader = subscriptionReader;
            _locationReader = locationReader;

            var today = timeProvider.Today;
            _min = today;
            _max = MaximumSchedule(today);
        }

        private DateTime MaximumSchedule(DateTime today)
        {
            var max = today.AddDays(7 * 4);

            while(max.DayOfWeek != DayOfWeek.Sunday)
            {
                max = max.AddDays(1);
            }

            return max;
        }

        public PlanOverview Handle(GetPlanOverviewForUserQuery query)
        {
            var courseTypeIds = (
                from subscription in _subscriptionReader.Entities
                where subscription.CustomerId == query.UserId
                select subscription.CourseTypeId).ToArray();

            return new PlanOverview
            {
                Locations = Plan(courseTypeIds).ToArray()
            };
        }

        private IEnumerable<PlanLocation> Plan(IEnumerable<Guid> courseTypeIds) =>
            from location in EagerLoadedLocations()
            where location.Schedules.Any(s =>
                s.ScheduleItems.Any(i =>
                    courseTypeIds.Contains(i.Course.CourseTypeId)))

            let groupedCourses = (
                from schedule in location.Schedules
                from item in schedule.ScheduleItems
                where courseTypeIds.Contains(item.Course.CourseTypeId)
                select item.Course).Distinct()

            let courses =
                from course in groupedCourses

                let dates =
                    from item in course.ScheduleItems
                    where item.Date >= _min
                    where item.Date <= _max
                    where item.Schedule.LocationId == location.Id
                    group item by item.Date into itemsByDate

                    let times =
                        from time in itemsByDate
                        select new PlanTime
                        {
                            Id = time.Id,
                            StartTime = time.StartTime,
                            EndTime = time.EndTime,
                            TrainerId = time.PersonalTrainerId,
                            Trainer = time.PersonalTrainerId.HasValue ? time.PersonalTrainer.Name : string.Empty,
                            Capacity = time.Course.Capacity,
                            Registrations = time.Agenda.Count()
                        }

                    select new PlanDate
                    {
                        Date = itemsByDate.Key,
                        Times = times.ToArray()
                    }

                select new PlanCourse
                {
                    Id = course.Id,
                    Name = course.Name,
                    Subscription = course.CourseType.Name,
                    Dates = dates.ToArray()
                }

            select new PlanLocation
            {
                Id = location.Id,
                Name = location.Name,
                Courses = courses.ToArray()
            };

        private IQueryable<LocationModel> EagerLoadedLocations()
        {
            var locationsQuery = _locationReader.Entities
                .Include(l => l.Schedules)
                .ThenInclude(s => s.ScheduleItems);

            locationsQuery.ThenInclude(i => i.Agenda);
            locationsQuery.ThenInclude(i => i.PersonalTrainer);
            locationsQuery.ThenInclude(i => i.Course.CourseType);

            return locationsQuery;
        }
    }
}
