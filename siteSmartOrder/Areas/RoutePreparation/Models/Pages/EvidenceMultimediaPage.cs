using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class EvidenceMultimediaPage : Page
    {
        public EvidenceMultimediaPage()
        {
            EvidenceMultimedias = new List<EvidenceMultimedia>();
        }

        public List<EvidenceMultimedia> EvidenceMultimedias { get; set; }
    }
}