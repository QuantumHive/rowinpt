using System;
using AlperAslanApps.Core;

namespace RowinPt.Contract.Models
{
    public class Course : IIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public Guid CourseTypeId { get; set; }
    }
}
