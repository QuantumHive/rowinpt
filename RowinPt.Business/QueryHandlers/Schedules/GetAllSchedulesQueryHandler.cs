using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Schedules
{
    internal sealed class GetAllSchedulesQueryHandler : IQueryHandler<GetAllQuery<Schedule>, IEnumerable<Schedule>>
    {
        private readonly IReader<ScheduleModel> _scheduleReader;

        public GetAllSchedulesQueryHandler(IReader<ScheduleModel> scheduleReader)
        {
            _scheduleReader = scheduleReader;
        }

        public IEnumerable<Schedule> Handle(GetAllQuery<Schedule> query)
        {
            return _scheduleReader.Entities.Select(schedule => new Schedule
            {
                Id = schedule.Id,
                Name = schedule.Name,
                LocationId = schedule.LocationId,
            });
        }
    }
}
