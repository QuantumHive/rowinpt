using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries.Plan
{
    public class GetPlanTimesForCoursePlanDateQuery : IQuery<IEnumerable<PlanTime>>
    {
        public GetPlanTimesForCoursePlanDateQuery(Guid locationId, Guid courseId, DateTime date)
        {
            LocationId = locationId;
            CourseId = courseId;
            Date = date;
        }

        public Guid LocationId { get; }
        public Guid CourseId { get; }
        public DateTime Date { get; }
    }
}
