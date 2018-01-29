using System.Collections.Generic;
namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class AlertPage : Page
    {
        public AlertPage()
        {
            Alerts = new List<Alert>();
        }

        public List<Alert> Alerts { get; set; }
    }
}