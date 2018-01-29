using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.NoticeRecharge.Models
{
    public class ViewModel<T,K>
    {
        public T MainClass { get; set; }
        public K HelperClass { get; set; }
    }
}