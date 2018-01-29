using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Infrastructure.Tools
{
    public class MileageRegex
    {
        public static readonly string DayOfWeekLength = @"^[0-9]{2,3}$";
        public static readonly string RouteLength = @"^[0-9]{1,5}$";
        public static readonly string BranchLength = @"^[0-9]{1,4}$";
        public static readonly string TravelsLength = @"^[0-9]{1,2}$";
    }
}