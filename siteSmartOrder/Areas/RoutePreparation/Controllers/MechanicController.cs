using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class MechanicController : Controller
    {
        private readonly IMechanicService _mechanicService;
        private readonly IJsonFactory _jsonFactory;

        public MechanicController(IMechanicService mechanicService, IJsonFactory jsonFactory)
        {
            _mechanicService = mechanicService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var mechanic = _mechanicService.Get(id);
                return _jsonFactory.Success(mechanic);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(MechanicFilter mechanicFilter)
        {
            try
            {
                var response = _mechanicService.Filter(mechanicFilter);
                return _jsonFactory.Success(response.Mechanics, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
