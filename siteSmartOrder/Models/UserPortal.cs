using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class UserPortal
    {
        public int userPortalId { set; get; }
        //public int? branchId { set; get; }
        //public string branchCode { set; get; }
        public string name { set; get; }
        public string nickname { set; get; }
        public string code { set; get; }
        public Branch branch { set; get; }
        
    }
}