using AlperAslanApps.Core;
using System;

namespace RowinPt.Domain
{
    public class AbsenceNotesModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }
        public Guid CompanyId { get; set; }

        public CustomerModel Customer { get; set; }
        public Guid CustomerId { get; set; }
        public string Notes { get; set; }
    }
}
