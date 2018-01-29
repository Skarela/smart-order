using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Dtos.Requests
{
    public class CampaignDatesRequest
    {
   
        public CampaignDate CampaignDate { get; set; }

    }


    public class CampaignDate
    {
        public int Id {get; set;}
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
