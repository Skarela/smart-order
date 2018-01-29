using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class WorkshopPage : Page
    {
        public WorkshopPage()
        {
            Workshops = new List<Workshop>();
        }

        public List<Workshop> Workshops { get; set; }
    }
}