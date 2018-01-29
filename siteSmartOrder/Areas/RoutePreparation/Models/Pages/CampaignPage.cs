using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CampaignPage : Page
    {
        public CampaignPage()
        {
            Campaigns = new List<Campaign>();
        }

        public List<Campaign> Campaigns { get; set; }
    }
}