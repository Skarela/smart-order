using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class AuditsContainer
   {

       public UserToAuditCampaign User { get; set; }
       public List<AuditsByCustomer> AuditsByCustomer { get; set; }
       public List<Customer> Pendings { get; set; }
       public int auditCampaignId { get; set; }
       public int userId { get; set; }
       public string branchCode { get; set; }
       public int statusAuditCampaign { get; set; }
   }
}