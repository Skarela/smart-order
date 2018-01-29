using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Controllers
{
    public class AuthorizeCustom : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Session["UserPortal"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}