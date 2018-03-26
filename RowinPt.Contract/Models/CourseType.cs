using System;
using AlperAslanApps.Core;

namespace RowinPt.Contract.Models
{
    public class CourseType : IIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
