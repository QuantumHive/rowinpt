using System;
using System.Collections.Generic;
using AlperAslanApps.Core;

namespace RowinPt.Domain
{
    public class CourseModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }

        public Guid CourseTypeId { get; set; }
        public CourseTypeModel CourseType { get; set; }

        public IEnumerable<ScheduleItemModel> ScheduleItems { get; set; }
    }
}
