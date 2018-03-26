using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Services
{
    [DebuggerStepThrough]
    public class SystemClockProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today => DateTime.Today;
    }
}
