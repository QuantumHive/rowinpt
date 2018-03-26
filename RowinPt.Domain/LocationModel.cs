using AlperAslanApps.Core;
using System;
using System.Collections.Generic;

namespace RowinPt.Domain
{
    public class LocationModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<ScheduleModel> Schedules { get; set; }
    }
}
