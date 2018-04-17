using System;
using System.Collections.Generic;
using AlperAslanApps.Core;

namespace RowinPt.Domain
{
    public class ScheduleModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }
        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public Guid LocationId { get; set; }
        public LocationModel Location { get; set; }
        public IEnumerable<ScheduleItemModel> ScheduleItems { get; set; }
    }
}
