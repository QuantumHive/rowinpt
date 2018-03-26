using System;

namespace AlperAslanApps.Core
{
    public static class UserContextExtensions
    {
        public static Guid GetId(this IUserContext userContext) => Guid.Parse(userContext.Id);
    }
}
