using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class MultimediaPage : Page
    {
        public MultimediaPage()
        {
            Multimedias = new List<Multimedia>();
        }

        public List<Multimedia> Multimedias { get; set; }
    }
}