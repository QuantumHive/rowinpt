using AlperAslanApps.Core;
using System;

namespace RowinPt.Domain
{
    public class AgendaModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }

        public Guid ScheduleItemId { get; set; }
        public Guid CustomerId { get; set; }

        public ScheduleItemModel ScheduleItem { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
