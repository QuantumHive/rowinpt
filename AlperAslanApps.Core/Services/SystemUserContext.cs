using System.Diagnostics;

namespace AlperAslanApps.Core.Services
{
    [DebuggerStepThrough]
    public class SystemUserContext : IUserContext
    {
        public string Id => "system";
    }
}
