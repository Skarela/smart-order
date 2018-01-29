using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
    public class AuditCampaign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string BranchCode { get; set; }

        public List<Branch> Branches { get; set; }

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