using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class NewCoolerMultimediaPage : Page
    {
        public NewCoolerMultimediaPage()
        {
            NewCoolerMultimedias = new List<NewCoolerMultimedia>();
        }

        public List<NewCoolerMultimedia> NewCoolerMultimedias { get; set; }
    }
}