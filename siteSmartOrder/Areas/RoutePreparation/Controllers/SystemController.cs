using System.Web.Mvc;
using siteSmartOrder.Controllers;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class SystemController : Controller
    {
        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult Version()
        {
            return View("Version");
        }

        #endregion
    }
}
