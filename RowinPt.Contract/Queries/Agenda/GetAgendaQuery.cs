using AlperAslanApps.Core;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries.Agenda
{
    public class GetAgendaQuery : IQuery<IEnumerable<Models.Agenda>>
    {
        public GetAgendaQuery(Guid locationId, DateTime date)
        {
            LocationId = locationId;
            Date = date;
        }

        public Guid LocationId { get; }
        public DateTime Date { get; }
    }
}
