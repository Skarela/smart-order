using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Areas.NoticeRecharge
{
    public class NoticeRechargeRegistration:AreaRegistration
    {
        public override string AreaName
        {
            get { return "NoticeRecharge"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("NoticeRecharge_default", "NoticeRecharge/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }
}