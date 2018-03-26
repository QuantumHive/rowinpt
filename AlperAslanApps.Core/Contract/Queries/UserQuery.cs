using System;

namespace AlperAslanApps.Core.Contract.Queries
{
    public abstract class UserQuery
    {
        public Guid UserId { get; set; }
    }
}
