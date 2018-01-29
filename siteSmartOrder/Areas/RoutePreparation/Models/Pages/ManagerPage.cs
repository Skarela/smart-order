using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class ManagerPage : Page
    {
        public ManagerPage()
        {
            Managers = new List<Manager>();
        }

        public List<Manager> Managers { get; set; }
    }
}