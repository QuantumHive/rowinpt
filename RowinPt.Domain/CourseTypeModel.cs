using System;
using System.Collections.Generic;
using AlperAslanApps.Core;

namespace RowinPt.Domain
{
    public class CourseTypeModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { set; get; }

        public string Name { get; set; }
        public int Capacity { get; set; }

        public IEnumerable<CourseModel> Courses { get; set; }
        public IEnumerable<SubscriptionModel> Subscriptions { get; set; }
    }
}
