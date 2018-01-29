using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Controllers;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Resolvers;

using siteSmartOrder.Areas.RoutePreparation.Models.ViewModels;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CampaignTemplateController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly ICampaignService _campaignService;
        private readonly ICampaignMultimediaService _campaignMultimediaService;
        private readonly IUserPortalService _userPortalService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public CampaignTemplateController(ISurveyService surveyRepository, ICampaignService campaignService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IUserPortalService userPortalService, ICampaignMultimediaService campaignMultimediaService)
        {
            _surveyRepository = surveyRepository;
            _campaignService = campaignService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _userPortalService = userPortalService;
            _campaignMultimediaService = campaignMultimediaService;
        }

        #region Get Request

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult New()
        {
            return View("New", new Campaign() { IsTemplate = true });
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var campaign = _campaignService.Get(id);
                var campaignMultimediaResponse = _campaignMultimediaService.Filter(new CampaignMultimediaFilter { CampaignId = id });
                campaign.CampaignMultimedias = campaignMultimediaResponse.CampaignMultimedias;
                campaign.CampaignMultimediaType = campaignMultimediaResponse.CampaignMultimedias.ResolverType();
                return View("Edit", campaign);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "CampaignTemplate");
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public JsonResult Create(Campaign campaign, Survey survey)
        {
            try
            {
                survey.Category.Id = (int)CategoryType.Campaign;
                survey.ShowPoints = true;
                _surveyRepository.Create(survey);
                campaign.SurveyId = survey.Id;
                _campaignService.Create(campaign);
                 _alertFactory.CreateSuccess(this, "Plantilla creada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() {
                    Message = "Plantilla creada con éxito!",
                    Success = true });
            }
            catch (Exception e)
            {

                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = e.Message,
                    Success = false
                });
            }
        }

        [HttpPost]
        public JsonResult Update(Campaign campaign, Survey survey)
        {
            try
            {
                 _surveyRepository.Copy(survey);
                campaign.SurveyId = survey.Id;
                _campaignService.Update(campaign);
                 _alertFactory.CreateSuccess(this, "Plantilla editada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = "Plantilla editada con éxito!",
                    Success = true
                });
            }
            catch (Exception e)
            {
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = e.Message,
                    Success = false
                });
            }
        }



        #endregion

        #region Delete Request
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _campaignService.Delete(id);
                return _jsonFactory.Success("Plantilla eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }
        #endregion
    }
}
