using System;

namespace siteSmartOrder.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        private const string CharSeparatedDate = "/";
        private const string DateFormat = "dd" + CharSeparatedDate + "M" + CharSeparatedDate + "yyyy";

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !IsNullOrEmpty(value);
        }

        public static bool IsEquals(this string value, string value2)
        {
            return value == value2;
        }

        public static bool IsNotEquals(this string value, string value2)
        {
            return value != value2;
        }

        public static int ConvertToint(string value)
        {
            try
            {
                var convert = Convert.ToInt32(value);
                return convert;
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime ConvertToDateTime(this string value)
        {
            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                return new DateTime();
            }

        }

        public static string ExtractOnlyDate(this string value)
        {
            return value.ConvertToDateTime().ToString(DateFormat);
        }

        public static int ExtractOnlyYear(this string value)
        {
            try
            {
                return ConvertToDateTime(value).Year;
            }
            catch
            {
                return 0;
            }
        }

        public static string ConvertToCalendarDates(this string date)
        {
            return String.Format("{0:MM/dd/yyyy HH:mm }", DateTime.Parse(date));
        }

        public static bool ConvertToBolean(this string value)
        {
            return Convert.ToBoolean(value);
        }
    }
}