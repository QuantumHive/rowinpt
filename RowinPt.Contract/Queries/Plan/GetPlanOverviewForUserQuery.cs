using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;

namespace RowinPt.Contract.Queries.Plan
{
    public class GetPlanOverviewForUserQuery : IQuery<PlanOverview>
    {
        public GetPlanOverviewForUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
