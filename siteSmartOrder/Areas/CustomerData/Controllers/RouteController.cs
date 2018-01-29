using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Areas.CustomerData.Models;
using siteSmartOrder.Areas.CustomerData.Interfaces;
using siteSmartOrder.Areas.CustomerData.Repositories;

namespace siteSmartOrder.Areas.CustomerData.Controllers
{
    public class RouteController : Controller
    {
        //
        // GET: /NoticeRecharge/Route/

        private IRouteRepository _routeRepository;

        public RouteController()
        {
            _routeRepository = new RouteRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(int branchId)
        {
            List<Route> routes = _routeRepository.GetByBranch(branchId);
            return Json(routes.OrderBy(r => Convert.ToInt32(r.Code)), JsonRequestBehavior.AllowGet);
        }

    }
}
