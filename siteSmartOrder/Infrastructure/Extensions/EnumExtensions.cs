using System;
using System.Collections.Generic;
using System.Linq;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;

namespace siteSmartOrder.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static List<Enumerator> ConvertToCollection(this Enum enumerator)
        {
            var type = enumerator.GetType();
            return Enum.GetNames(type)
                    .Select(name => new Enumerator { Name = name, Value = (int)Enum.Parse(type, name) })
                    .ToList();
        }
    }
}