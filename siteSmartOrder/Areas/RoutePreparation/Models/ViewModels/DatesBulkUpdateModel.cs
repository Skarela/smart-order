using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Areas.RoutePreparation.Models.ViewModels
{
    public class CampaignList
    {
        public int id { get; set; }
        public string branchName { get; set; }
        public string surveyName { get; set; }
    }

    public class DatesBulkUpdateModel
    {
        public List<CampaignList> CampaignList { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
