using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class UnitPage : Page
    {
        public UnitPage()
        {
            Units = new List<Unit>();
        }

        public List<Unit> Units { get; set; }
    }
}