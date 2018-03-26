using System;
using System.Diagnostics;

namespace AlperAslanApps.Core
{
    public static class Requires
    {
        [DebuggerStepThrough]
        public static void ThrowIfNull<T>(this T instance, string parameterName)
            where T : class
        {
            if (instance == null)
            {
                throw new ArgumentException($"{parameterName} is null");
            }
        }
    }
}
