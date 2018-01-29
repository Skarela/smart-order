using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class UserRoutePage : Page
    {
        public UserRoutePage()
        {
            Routes = new List<UserRoute>();
        }

        public List<UserRoute> Routes { get; set; }
    }
}