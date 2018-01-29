using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class WorkshopReplyPage : Page
    {
        public WorkshopReplyPage()
        {
            WorkshopReplies = new List<WorkshopReply>();
        }

        public List<WorkshopReply> WorkshopReplies { get; set; }
    }
}