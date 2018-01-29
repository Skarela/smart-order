using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CampaignMultimediaPage : Page
    {
        public CampaignMultimediaPage()
        {
            CampaignMultimedias = new List<CampaignMultimedia>();
        }

        public List<CampaignMultimedia> CampaignMultimedias { get; set; }
    }
}