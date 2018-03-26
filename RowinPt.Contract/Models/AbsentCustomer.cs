using System;
using System.Collections.Generic;
using AlperAslanApps.Core;

namespace RowinPt.Contract.Models
{
    public class AbsentCustomer : IIdentifier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastSeen { get; set; }
    }

    public class AbsentCustomerDetails : IIdentifier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public IEnumerable<AbsentCustomerActivity> Activity { get; set; }

        public string Notes { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }

    public class AbsentCustomerActivity
    {
        public Guid CourseTypeId { get; set; }
        public DateTime? LastSeen { get; set; }
        public string Subscription { get; set; }
    }
}
