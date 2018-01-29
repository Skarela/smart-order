using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class NewCoolerController : Controller
    {
        private readonly INewCoolerService _newCooleryService;
        private readonly IJsonFactory _jsonFactory;

        public NewCoolerController(INewCoolerService newCooleryService, IJsonFactory jsonFactory)
        {
            _newCooleryService = newCooleryService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var newCooler = _newCooleryService.Get(id);
                return _jsonFactory.Success(newCooler);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(NewCoolerFilter newCooleryFilter)
        {
            try
            {
                var response = _newCooleryService.Filter(newCooleryFilter);
                return _jsonFactory.Success(response.NewCoolers, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
