using System;
using AlperAslanApps.Core;

namespace RowinPt.Contract.Models
{
    public class ScheduleItem : IIdentifier
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public Guid? PersonalTrainerId { get; set; }
        public Guid CourseId { get; set; }
        public int Repeat { get; set; }
    }
}
