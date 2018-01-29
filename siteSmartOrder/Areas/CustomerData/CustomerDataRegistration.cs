using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Areas.CustomerData
{
    public class CustomerDataRegistration:AreaRegistration
    {
        public override string AreaName
        {
            get { return "CustomerData"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("CustomerData_default", "CustomerData/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }
}