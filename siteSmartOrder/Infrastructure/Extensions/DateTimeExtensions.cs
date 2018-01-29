using System;
using System.Globalization;

namespace siteSmartOrder.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsValidDate(this DateTime date)
        {
            return date > DateTime.MinValue && date < DateTime.MaxValue;
        }

        public static bool IsNotValidDate(this DateTime date)
        {
            return !date.IsValidDate();
        }

        public static string GetTicksNow()
        {
            return DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
        }
    }
}