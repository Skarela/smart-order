using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace siteSmartOrder.Infrastructure.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsEqualTo(this int intValue, int valueToCompare)
        {
            return intValue == valueToCompare;
        }

        public static bool IsNotEqualTo(this int intValue, int valueToCompare)
        {
            return intValue != valueToCompare;
        }

        public static bool IsGreaterThan(this int intValue, int valueToCompare)
        {
            return intValue > valueToCompare;
        }

        public static bool IsLessThan(this int intValue, int valueToCompare)
        {
            return intValue < valueToCompare;
        }

        public static bool IsEqualToZero(this int intValue)
        {
            return intValue == 0;
        }

        public static bool IsGreaterThanZero(this int intValue)
        {
            return intValue > 0;
        }

        public static T ConvertToEnum<T>(this int value) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static string ConvertToString(this int intValue)
        {
            return intValue.ToString(CultureInfo.InvariantCulture);
        }

        public static string ConvertToAmmount(this int intValue)
        {
            return string.Format("{0:C}", intValue);
        }

        public static List<string> ConvertToAmmount(this List<int> values)
        {
            return values.Select(x => string.Format("{0:C}", x)).ToList();
        }
    }
}
