using System.Collections.Generic;
namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class AlertConfigurationPage : Page
    {
        public AlertConfigurationPage()
        {
            AlertConfigurations = new List<AlertConfiguration>();
        }

        public List<AlertConfiguration> AlertConfigurations { get; set; }
    }
}