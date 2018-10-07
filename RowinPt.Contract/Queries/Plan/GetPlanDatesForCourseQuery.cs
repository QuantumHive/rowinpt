using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries.Plan
{
    public class GetPlanDatesForCourseQuery : IQuery<IEnumerable<PlanDate>>
    {
        public GetPlanDatesForCourseQuery(Guid courseId, Guid locationId)
        {
            CourseId = courseId;
            LocationId = locationId;
        }

        public Guid CourseId { get; }
        public Guid LocationId { get; }
    }
}
