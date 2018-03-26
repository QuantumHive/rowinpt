using AlperAslanApps.Core;
using System;

namespace RowinPt.Contract.Queries.Users
{
    public class IsUserCustomerQuery : IQuery<bool>
    {
        public IsUserCustomerQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
