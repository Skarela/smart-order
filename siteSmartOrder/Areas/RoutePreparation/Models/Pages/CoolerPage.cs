using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CoolerPage : Page
    {
        public CoolerPage()
        {
            Coolers = new List<Cooler>();
        }

        public List<Cooler> Coolers { get; set; }
    }
}