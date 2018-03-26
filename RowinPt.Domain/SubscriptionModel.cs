using AlperAslanApps.Core;
using System;

namespace RowinPt.Domain
{
    public class SubscriptionModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }

        public string Notes { get; set; }
        public int WeeklyCredits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? RecentEntry { get; set; }

        public Guid CustomerId { get; set; }
        public Guid CourseTypeId { get; set; }

        public CustomerModel Customer { get; set; }
        public CourseTypeModel CourseType { get; set; }
    }
}
