using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class WorkshopReplyMultimediaPage : Page
    {
        public WorkshopReplyMultimediaPage()
        {
            WorkshopReplyMultimedias = new List<WorkshopReplyMultimedia>();
        }

        public List<WorkshopReplyMultimedia> WorkshopReplyMultimedias { get; set; }
    }
}