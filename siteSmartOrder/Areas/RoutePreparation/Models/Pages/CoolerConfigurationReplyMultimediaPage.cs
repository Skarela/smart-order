using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CoolerConfigurationReplyMultimediaPage : Page
    {
        public CoolerConfigurationReplyMultimediaPage()
        {
            CoolerConfigurationReplyMultimedias = new List<CoolerConfigurationReplyMultimedia>();
        }

        public List<CoolerConfigurationReplyMultimedia> CoolerConfigurationReplyMultimedias { get; set; }
    }
}