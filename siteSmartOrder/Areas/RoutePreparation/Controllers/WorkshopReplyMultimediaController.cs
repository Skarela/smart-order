using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class WorkshopReplyMultimediaController : Controller
    {
        private readonly IWorkshopReplyMultimediaService _workshopReplyMultimediaService;
        private readonly IJsonFactory _jsonFactory;

        public WorkshopReplyMultimediaController(IWorkshopReplyMultimediaService workshopReplyMultimediaService, IJsonFactory jsonFactory)
        {
            _workshopReplyMultimediaService = workshopReplyMultimediaService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Filter(WorkshopReplyMultimediaFilter workshopReplyMultimediaFilter)
        {
            try
            {
                var response = _workshopReplyMultimediaService.Filter(workshopReplyMultimediaFilter);
                return _jsonFactory.Success(response.WorkshopReplyMultimedias, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
