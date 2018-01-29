using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class UserPortalPage : Page
    {
        public UserPortalPage()
        {
            UserPortals = new List<UserPortal>();
        }

        public List<UserPortal> UserPortals { get; set; }
    }
}