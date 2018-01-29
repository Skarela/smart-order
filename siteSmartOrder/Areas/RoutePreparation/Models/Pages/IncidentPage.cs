using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class IncidentPage : Page
    {
        public IncidentPage()
        {
            Incidents = new List<Incident>();
        }

        public List<Incident> Incidents { get; set; }
    }
}