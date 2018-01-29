using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class AlertConfigurationController : Controller
    {
        private readonly IAlertConfigurationService _alertConfigurationyService;
        private readonly IJsonFactory _jsonFactory;

        public AlertConfigurationController(IAlertConfigurationService alertConfigurationyService, IJsonFactory jsonFactory)
        {
            _alertConfigurationyService = alertConfigurationyService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var alertConfiguration = _alertConfigurationyService.Get(id);
                return _jsonFactory.Success(alertConfiguration);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(AlertConfigurationFilter alertConfigurationyFilter)
        {
            try
            {
                var response = _alertConfigurationyService.Filter(alertConfigurationyFilter);
                return _jsonFactory.Success(response.AlertConfigurations, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
