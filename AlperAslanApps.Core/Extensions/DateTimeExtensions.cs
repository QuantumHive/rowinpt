using System;

namespace AlperAslanApps.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date)
        {
            var start = date.Date;
            while (start.DayOfWeek != DayOfWeek.Sunday)
            {
                start = start.AddDays(-1);
            }
            return start;
        }

        public static DateTime StartOfMonth(this DateTime date)
        {
            var startOfDay = date.Date;
            return startOfDay.AddDays((startOfDay.Day * -1) + 1);
        }
    }
}
