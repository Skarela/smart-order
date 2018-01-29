using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class AuditCampaignContainer
   {
      public List<AuditCampaign> AuditCampaigns { set; get; }

      public int CountRows { set; get; }
   }
}