using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Customer
    {
        public int customerId { set; get; }
        public int routeId { set; get; }
        public string code { set; get; }
        public string name { set; get; }
        public string contact { set; get; }
        public string address { set; get; }
        public string routeName { set; get; }
    }
}