using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Report
    {
        public int reportId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string server { get; set; }
        public string path { get; set; }
        public int type { get; set; }
    }
}