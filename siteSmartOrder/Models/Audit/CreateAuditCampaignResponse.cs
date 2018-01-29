using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class CreateAuditCampaignResponse
   {
       public CreateAuditCampaignResponse()
       {
           UsersToHaveActiveCampaign = new List<string>();
       }

       public int Id { get; set; }

       public List<string> UsersToHaveActiveCampaign { get; set; }
   }
}