using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Plan;
using RowinPt.Domain;
using System.Linq;
using System.Collections.Generic;

namespace RowinPt.Business.QueryHandlers.Plan
{
    internal sealed class GetPlanTimesForCoursePlanDateQueryHandler : IQueryHandler<GetPlanTimesForCoursePlanDateQuery, IEnumerable<PlanTime>>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;

        public GetPlanTimesForCoursePlanDateQueryHandler(
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _scheduleItemReader = scheduleItemReader;
        }

        public IEnumerable<PlanTime> Handle(GetPlanTimesForCoursePlanDateQuery query)
        {
            var times =
                from time in _scheduleItemReader.Entities
                where time.Schedule.LocationId == query.LocationId
                where time.CourseId == query.CourseId
                where time.Date == query.Date
                select new PlanTime
                {
                    Id = time.Id,
                    StartTime = time.StartTime,
                    EndTime = time.EndTime,
                    TrainerId = time.PersonalTrainerId,
                    Trainer = time.PersonalTrainerId.HasValue ? time.PersonalTrainer.Name : string.Empty,
                    Capacity = time.Course.Capacity,
                    Registrations = time.Agenda.Count()
                };

            return times.ToArray();
        }
    }
}
