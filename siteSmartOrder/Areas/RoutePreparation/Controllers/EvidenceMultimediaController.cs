using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class EvidenceMultimediaController : Controller
    {
        private readonly IEvidenceMultimediaService _evidenceMultimediaService;
        private readonly IJsonFactory _jsonFactory;

        public EvidenceMultimediaController(IJsonFactory jsonFactory, IEvidenceMultimediaService evidenceMultimediaService)
        {
            _jsonFactory = jsonFactory;
            _evidenceMultimediaService = evidenceMultimediaService;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Filter(EvidenceMultimediaFilter evidenceMultimediaFilter)
        {
            try
            {
                var response = _evidenceMultimediaService.Filter(evidenceMultimediaFilter);
                return _jsonFactory.Success(response.EvidenceMultimedias, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
