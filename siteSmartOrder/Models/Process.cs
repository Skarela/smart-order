using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Process
    {
        public int ProcessId { get; set; }
        public string StartProcess { get; set; }
        public string EndProcess { get; set; }
        public int Percent { get; set; }
    }
}