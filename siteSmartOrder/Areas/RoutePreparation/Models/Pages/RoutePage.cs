using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class RoutePage : Page
    {
        public RoutePage()
        {
            Routes = new List<Route>();
        }

        public List<Route> Routes { get; set; }
    }
}