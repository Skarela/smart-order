namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CampaignMultimediaFilter : Filter
    {
        public CampaignMultimediaFilter()
        {
            CampaignId = 0;
        }

        public int CampaignId { get; set; }
    }
}