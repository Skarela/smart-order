using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class ChecklistPage : Page
    {
        public ChecklistPage()
        {
            CheckLists = new List<Checklist>();
        }

        public List<Checklist> CheckLists { get; set; }
    }
}