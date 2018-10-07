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

        public GetPlanOverviewForUserQueryHandler(
            IReader<SubscriptionModel> subscriptionReader,
            IReader<LocationModel> locationReader,
            ITimeProvider timeProvider)
        {
            _subscriptionReader = subscriptionReader;
            _locationReader = locationReader;
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
                select new PlanCourse
                {
                    Id = course.Id,
                    Name = course.Name,
                    Subscription = course.CourseType.Name,
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
                .ThenInclude(s => s.ScheduleItems)
                .ThenInclude(i => i.Course.CourseType);

            return locationsQuery;
        }
    }
}
