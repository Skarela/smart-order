using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class ApplyAssignedSurveyController : Controller
    {
        private readonly IApplyAssignedSurveyService _applyAssignedSurveyService;
        private readonly IJsonFactory _jsonFactory;

        public ApplyAssignedSurveyController(IApplyAssignedSurveyService applyAssignedSurveyService, IJsonFactory jsonFactory)
        {
            _applyAssignedSurveyService = applyAssignedSurveyService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var assignedSurvey = _applyAssignedSurveyService.GetFlat(id);
                return _jsonFactory.Success(assignedSurvey);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
