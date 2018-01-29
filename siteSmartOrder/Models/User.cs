using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class User
    {
        public int userId { set; get; }
        public int? routeId { set; get; }
        public string deviceId { set; get; }
        public string routeName { set; get; }
        public string code { set; get; }
        public string name { set; get; }
        public int type { set; get; }
        public int ClosureTypeId { set; get; }
    }
}