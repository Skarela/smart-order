using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class ExtendAuditCampaign
   {
       public ExtendAuditCampaign()
       {
           Customers = new List<UserToAuditCampaign>();
       }

       public int Id { get; set; }

       public string Name { get; set; }

       public string StartDate { get; set; }

       public string EndDate { get; set; }

       public List<UserToAuditCampaign> Customers { get; set; }
   }
}