using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CoolerConfigurationPage : Page
    {
        public CoolerConfigurationPage()
        {
            CoolerConfigurations = new List<CoolerConfiguration>();
        }

        public List<CoolerConfiguration> CoolerConfigurations { get; set; }
    }
}