using System.Collections.Generic;
namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class SosAlertPage : Page
    {
        public SosAlertPage()
        {
            Alerts = new List<SosAlert>();
        }

        public List<SosAlert> Alerts { get; set; }
    }
}