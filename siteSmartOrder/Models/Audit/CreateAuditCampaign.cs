using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class CreateAuditCampaign
   {
       public CreateAuditCampaign()
       {
           UserIds = new List<int>();
       }

       public string Name { get; set; }

       public string StartDate { get; set; }

       public string EndDate { get; set; }

       public string BranchId { get; set; }

       public List<Branch> Branches { get; set; }

       public List<int> UserIds { get; set; }

       public bool ViewCreate { get; set; }
   }
}