using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CampaignReplyPage : Page
    {
        public CampaignReplyPage()
        {
            CampaignReplies = new List<CampaignReply>();
        }

        public List<CampaignReply> CampaignReplies { get; set; }
    }
}