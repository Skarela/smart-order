using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class PriceListDetail
    {
        public int priceListId { set; get; }
        public int productId { set; get; }
        public string productCode { set; get; }
        public string productName { set; get; }
        public double price { set; get; }
    }
}