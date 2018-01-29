using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CoolerConfigurationReplyMultimediaController : Controller
    {
        private readonly ICoolerConfigurationReplyMultimediaService _coolerConfigurationReplyMultimediaService;
        private readonly IJsonFactory _jsonFactory;

        public CoolerConfigurationReplyMultimediaController(ICoolerConfigurationReplyMultimediaService coolerConfigurationReplyMultimediaService, IJsonFactory jsonFactory)
        {
            _coolerConfigurationReplyMultimediaService = coolerConfigurationReplyMultimediaService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Filter(CoolerConfigurationReplyMultimediaFilter coolerConfigurationReplyMultimediaFilter)
        {
            try
            {
                var response = _coolerConfigurationReplyMultimediaService.Filter(coolerConfigurationReplyMultimediaFilter);
                return _jsonFactory.Success(response.CoolerConfigurationReplyMultimedias, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
