using System;

namespace AlperAslanApps.Core
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
