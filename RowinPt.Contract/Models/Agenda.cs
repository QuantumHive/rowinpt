using AlperAslanApps.Core;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Models
{
    public class Agenda : IIdentifier
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Course { get; set; }
        public string Trainer { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int Registrations { get; set; }

        public IEnumerable<UserRegistration> Users { get; set; }
    }

    public class UserRegistration : IIdentifier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
