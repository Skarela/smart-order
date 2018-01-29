using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class MechanicPage : Page
    {
        public MechanicPage()
        {
            Mechanics = new List<Mechanic>();
        }

        public List<Mechanic> Mechanics { get; set; }
    }
}