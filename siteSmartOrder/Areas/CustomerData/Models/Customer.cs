using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.CustomerData.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Ftr { get; set; }
        public string BusinessName { get; set; }
        public string FiscalAddress { get; set; }
        public bool Status { get; set; }
        public bool HelpStatus { get; set; }
        
        //private bool _OldStatus;
        public bool OldStatus
        {
            get
            {
                //_OldStatus = Status;
                return Status;
            }
        }
    }
}