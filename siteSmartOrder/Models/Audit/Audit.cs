using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class Audit
   {
       public int Id { get; set; }
       public string Comment { get; set; }
       public double Latitude { get; set; }
       public double Longitude { get; set; }
       public string ClientDate { get; set; }
       public Customer Customer { get; set; }
       public List<Asset> Assets { get; set; }
       public int Status { get; set; }
       //public StatusPercent StatusPercent()
       //{
       //    StatusPercent statusPercent;
       //    if (CountUsers > 0)
       //    {

       //        var percentDone = (CountFinalizeUsers / (float)CountUsers) * 100;
       //        var percentFail = DateTime.Now > DateTime.Parse(EndDate) ? 100 - percentDone : 0;
       //        var done = CountFinalizeUsers;
       //        var total = CountUsers;
       //        statusPercent = new StatusPercent(percentDone, percentFail, 0, done, total);

       //    }
       //    else
       //    {
       //        var percentFail = DateTime.Now > DateTime.Parse(EndDate) ? 100 : 0;
       //        var total = CountUsers;
       //        statusPercent = new StatusPercent(0, percentFail, 0, 0, total);
       //    }
       //    return statusPercent;
       //}
   }
}