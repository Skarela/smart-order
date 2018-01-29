using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly IJsonFactory _jsonFactory;

        public RouteController(IRouteService routeService, IJsonFactory jsonFactory)
        {
            _routeService = routeService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var route = _routeService.Get(id);
                return _jsonFactory.Success(route);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(RouteFilter routeFilter)
        {
            try
            {
                var response = _routeService.Filter(routeFilter);
                return _jsonFactory.Success(response.Routes, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult FilterByUser(UserRouteFilter routeFilter)
        {
            try
            {
                var response = _routeService.FilterByUser(routeFilter);
                return _jsonFactory.Success(response.Routes, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
