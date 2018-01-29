using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unityService;
        private readonly IJsonFactory _jsonFactory;

        public UnitController(IUnitService unityService, IJsonFactory jsonFactory)
        {
            _unityService = unityService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var unit = _unityService.Get(id);
                return _jsonFactory.Success(unit);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(UnitFilter unityFilter)
        {
            try
            {
                var response = _unityService.Filter(unityFilter);
                return _jsonFactory.Success(response.Units, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
