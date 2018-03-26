using System;

namespace AlperAslanApps.Core
{
    public interface IScopeStarter
    {
        IDisposable BeginScope();
    }
}
