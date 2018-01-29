using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class TableObject<T>
    {
        public T Data { set; get; }
        public int DataCount { set; get; }
    }
}