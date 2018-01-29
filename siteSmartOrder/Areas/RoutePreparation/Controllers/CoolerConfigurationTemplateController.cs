using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Controllers;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models.ViewModels;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CoolerConfigurationTemplateController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly ICoolerConfigurationService _coolerConfigurationService;
        private readonly IBranchService _branchService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public CoolerConfigurationTemplateController(ISurveyService surveyRepository, ICoolerConfigurationService coolerConfigurationService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IBranchService branchService)
        {
            _surveyRepository = surveyRepository;
            _coolerConfigurationService = coolerConfigurationService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _branchService = branchService;
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
            return View("New", new CoolerConfiguration() { IsTemplate = true});
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var coolerConfiguration = _coolerConfigurationService.Get(id);
                return View("Edit", coolerConfiguration);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "CoolerConfigurationTemplate");
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public JsonResult Create(CoolerConfiguration coolerConfiguration, Survey survey)
        {
            try
            {
                survey.Category.Id = (int)CategoryType.CoolerConfiguration;
                _surveyRepository.Create(survey);
                coolerConfiguration.SurveyId = survey.Id;
                _coolerConfigurationService.Create(coolerConfiguration);
                _alertFactory.CreateSuccess(this, "Plantilla de Revisión de Enfriadores creada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = "Plantilla de Revisión de Enfriadores creada con éxito!",
                    Success = true
                });
            }
            catch (Exception e)
            {
                //_alertFactory.CreateFailure(this, e.Message);
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = e.Message,
                    Success = false
                });
            }
        }

        [HttpPost]
        public JsonResult Update(CoolerConfiguration coolerConfiguration, Survey survey)
        {
            try
            {
                survey.Category.Id = (int)CategoryType.CoolerConfiguration;
                coolerConfiguration.SurveyId = survey.Id;
                _coolerConfigurationService.Update(coolerConfiguration);
                _surveyRepository.Copy(survey);
                coolerConfiguration.SurveyId = survey.Id;
                _coolerConfigurationService.Update(coolerConfiguration);
                _alertFactory.CreateSuccess(this, "Plantilla de Revisión de Enfriadores editada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = "Plantilla de Revisión de Enfriadores editada con éxito!",
                    Success = true
                });
            }
            catch (Exception e)
            {
                //_alertFactory.CreateFailure(this, e.Message); 
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
                _coolerConfigurationService.Delete(id);
                return _jsonFactory.Success("Template Revisión de enfriadores eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
