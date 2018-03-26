using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries
{
    public class GetAllScheduleItemsByScheduleIdQuery : IQuery<IEnumerable<ScheduleItem>>
    {
        public GetAllScheduleItemsByScheduleIdQuery(Guid scheduleId)
        {
            ScheduleId = scheduleId;
        }

        public Guid ScheduleId { get; }
    }
}
