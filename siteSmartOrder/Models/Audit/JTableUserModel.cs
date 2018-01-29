using System.Collections.Generic;
using System.Linq;

namespace siteSmartOrder.Models.Audit
{
    public class JTableUserModel
    {
        public int AuditCampaignId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int UserId { get; set; }
        public string BranchCode { get; set; }
        public string User { get; set; }
    }
}