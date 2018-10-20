using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Models
{
    public class PlanOverview
    {
        public IEnumerable<PlanLocation> Locations { get; set; }
    }

    public class PlanLocation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PlanCourse> Courses { get; set; }
    }

    public class PlanCourse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subscription { get; set; }
    }

    public class PlanDate
    {
        public DateTime Date { get; set; }
    }

    public class PlanTime
    {
        public Guid Id { get; set; }
        public Guid? TrainerId { get; set; }
        public string Trainer { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public int Registrations { get; set; }
    }
}
