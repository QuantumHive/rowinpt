using System;
using System.Collections.Generic;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;

namespace RowinPt.Contract.Models
{
    public class Customer : IIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }

        public int? Length { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Goal { get; set; }
        public DateTime? LastSeen { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }

        public class Subscription
        {
            public Guid CourseTypeId { get; set; }
            public int Credits { get; set; }
            public string Notes { get; set; }
            public DateTime StartDate { get; set; }
        }
    }
}
