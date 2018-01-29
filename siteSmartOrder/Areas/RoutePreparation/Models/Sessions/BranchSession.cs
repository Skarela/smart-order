using System.Collections.Generic;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Sessions
{
    public class BranchSession : Page
    {
        public BranchSession()
        {
            Branches = new List<Branch>();
            SelectedBranch = 0;
        }

        public List<Branch> Branches { get; set; }
        public int SelectedBranch { get; set; }
    }
}