using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
    public class UserToAuditCampaignContainer
    {
        public List<UserToAuditCampaign> Users { set; get; }

        public int CountRows { set; get; }
    }
}