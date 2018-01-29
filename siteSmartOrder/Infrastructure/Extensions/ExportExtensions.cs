using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;

namespace siteSmartOrder.Infrastructure.Extensions
{
    public static class ExportFactory
    {
        public static string ConcatRows<T>(this string line, int spaceNumber, string propertiesCommaSeparated, IEnumerable<T> objects)
        {
            line = string.Concat(line, "\n");
            foreach (var objectRetrieved in objects)
                line = line.ConcatRow(spaceNumber, propertiesCommaSeparated, objectRetrieved);
            return line;
        }

        public static string ConcatRow<T>(this string line, int spaceNumber, string propertiesCommaSeparated,T objectRetrieved)
        {
            var stringToConcat = string.Empty;
            var propertiesToRetrieve = propertiesCommaSeparated.Split(',').ToList();
            propertiesToRetrieve.ForEach(property =>
            {
                property = property.Replace(" ", "");
                var valueRetrieved = objectRetrieved.GetType().GetProperty(property).GetValue(objectRetrieved, null);
                var valueString = valueRetrieved.IsNotNull() ? valueRetrieved.ToString() : "";
                var valueWithoutCommas = valueString.Replace(",", "");
                var comma = stringToConcat.IsEmpty() ? string.Empty : ",";
                stringToConcat += string.Concat(comma, valueWithoutCommas);
            });

            return line.ConcatRow(spaceNumber, stringToConcat);
        }

        public static string ConcatRow(this string line, int spaceNumber, string row)
        {
            var commans = string.Concat(Enumerable.Repeat(",", spaceNumber));
            row = string.Concat(commans, row, "\n");
            return string.Concat(line, row);
        }
    }
}
