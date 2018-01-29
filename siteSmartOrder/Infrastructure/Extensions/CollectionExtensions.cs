using System.Collections.Generic;
using System.Linq;

namespace siteSmartOrder.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static bool Exist(this List<string> values, string valueToCompare)
        {
            return values.Any(value => value.Equals(valueToCompare));
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}


