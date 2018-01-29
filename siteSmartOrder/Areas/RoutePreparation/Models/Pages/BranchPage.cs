using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class BranchPage : Page
    {
        public BranchPage()
        {
            Branches = new List<Branch>();
        }

        public List<Branch> Branches { get; set; }
    }
}