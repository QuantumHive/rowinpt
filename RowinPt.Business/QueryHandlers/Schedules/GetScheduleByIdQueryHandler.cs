using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.Schedules
{
    internal sealed class GetScheduleByIdQueryHandler : IQueryHandler<GetByIdQuery<Schedule>, Schedule>
    {
        private readonly IReader<ScheduleModel> _scheduleReader;

        public GetScheduleByIdQueryHandler(IReader<ScheduleModel> scheduleReader)
        {
            _scheduleReader = scheduleReader;
        }

        public Schedule Handle(GetByIdQuery<Schedule> query)
        {
            var schedule = _scheduleReader.GetById(query.Id);

            return new Schedule
            {
                Id = schedule.Id,
                Name = schedule.Name,
                LocationId = schedule.LocationId,
            };
        }
    }
}
