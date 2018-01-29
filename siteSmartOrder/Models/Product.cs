using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Product
    {
        public int productId { set; get; }
        public string code { set; get; }
        public string name { set; get; }
        public string category { set; get; }
        public string brand { set; get; }
    }
}