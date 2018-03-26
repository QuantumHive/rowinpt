using AlperAslanApps.Core;
using System;

namespace RowinPt.Contract.Models
{
    public class CustomerAgenda : IIdentifier
    {
        public Guid Id { get; set; }
        public string Course { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Trainer { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int Registrations { get; set; }
    }
}
