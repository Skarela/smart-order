using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.CustomerData.Models
{
    public class Branch
    {
        public int BranchId { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
    }
}