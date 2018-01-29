using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class IncidencePage : Page
    {
        public IncidencePage()
        {
            Incidences = new List<Incidence>();
        }

        public List<Incidence> Incidences { get; set; }
    }
}