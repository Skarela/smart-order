using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class PriceList
    {
        public int priceListId { set; get; }
        public string name { set; get; }
        public string code { set; get; }
        public bool isMaster { set; get; }
        public string maester { set; get; }
    }
}