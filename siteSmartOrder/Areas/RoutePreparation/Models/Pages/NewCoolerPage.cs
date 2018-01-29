using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class NewCoolerPage : Page
    {
        public NewCoolerPage()
        {
            NewCoolers = new List<NewCooler>();
        }

        public List<NewCooler> NewCoolers { get; set; }
    }
}