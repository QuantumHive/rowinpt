using System.Collections.Generic;

namespace AlperAslanApps.Core
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> ToSingle<TResult>(this TResult result)
        {
            yield return result;
        }
    }
}
