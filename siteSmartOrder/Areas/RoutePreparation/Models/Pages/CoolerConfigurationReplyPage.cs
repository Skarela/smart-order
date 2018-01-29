using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CoolerConfigurationReplyPage : Page
    {
        public CoolerConfigurationReplyPage()
        {
            CoolerConfigurationReplies = new List<CoolerConfigurationReply>();
        }

        public List<CoolerConfigurationReply> CoolerConfigurationReplies { get; set; }
    }
}