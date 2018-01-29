using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class ChecklistReplyPage : Page
    {
        public ChecklistReplyPage()
        {
            ChecklistReplies = new List<ChecklistReply>();
        }

        public List<ChecklistReply> ChecklistReplies { get; set; }
    }
}