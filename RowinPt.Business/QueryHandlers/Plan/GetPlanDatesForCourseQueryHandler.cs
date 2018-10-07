using RowinPt.Contract.Models;
using System.Linq;
using System.Collections.Generic;
using RowinPt.Contract.Queries.Plan;
using AlperAslanApps.Core;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.QueryHandlers.Plan
{
    internal sealed class GetPlanDatesForCourseQueryHandler : IQueryHandler<GetPlanDatesForCourseQuery, IEnumerable<PlanDate>>
    {
        private readonly IReader<CourseModel> _courseReader;

        private readonly DateTime _min;
        private readonly DateTime _max;

        public GetPlanDatesForCourseQueryHandler(
            IReader<CourseModel> courseReader,
            ITimeProvider timeProvider)
        {
            _courseReader = courseReader;

            var today = timeProvider.Today;
            _min = today;
            _max = MaximumSchedule(today);
        }

        private DateTime MaximumSchedule(DateTime today)
        {
            var max = today.AddDays(7 * 4);

            while (max.DayOfWeek != DayOfWeek.Sunday)
            {
                max = max.AddDays(1);
            }

            return max;
        }

        public IEnumerable<PlanDate> Handle(GetPlanDatesForCourseQuery query)
        {
            var dates =
                from course in _courseReader.Entities
                where course.Id == query.CourseId
                from item in course.ScheduleItems
                where item.Date >= _min
                where item.Date <= _max
                where item.Schedule.LocationId == query.LocationId
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
                };

            return dates.ToArray();
        }
    }
}
