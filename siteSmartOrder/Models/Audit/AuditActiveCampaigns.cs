using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models.Audit
{
    public class AuditActiveCampaigns
    {
        public int AuditCampaignId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
        public List<Customer> Customers { get; set; }
    }
}