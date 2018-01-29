using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Enums;

using siteSmartOrder.Areas.RoutePreparation.Models.ViewModels;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CheckListTemplateController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly IChecklistService _checklistService;
        private readonly IBranchService _branchService;
        private readonly IUserPortalService _userPortalService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public CheckListTemplateController(ISurveyService surveyRepository, IChecklistService checklistService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IBranchService branchService, IUserPortalService userPortalService)
        {
            _surveyRepository = surveyRepository;
            _checklistService = checklistService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _branchService = branchService;
            _userPortalService = userPortalService;
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
            return View("New", new Checklist() { IsTemplate = true });
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var checklist = _checklistService.Get(id);
                return View("Edit", checklist);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "CheckListTemplate");
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public JsonResult Create(Checklist checklist, Survey survey)
        {
            try
            {
                survey.Category.Id = (int)CategoryType.Checklist;
                _surveyRepository.Create(survey);
                checklist.SurveyId = survey.Id;
                _checklistService.Create(checklist);
               _alertFactory.CreateSuccess(this, "Plantilla de Revisión de Unidad creada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { 
                    Message = "Plantilla de Revisión de Unidad creada con éxito!", Success = true });

            }
            catch (Exception e)
            {
                //   _alertFactory.CreateFailure(this, e.Message);
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = e.Message,
                    Success = false
                });

            }
        }

        [HttpPost]
        public JsonResult Update(Checklist checklist, Survey survey)
        {
            try
            {
                checklist.SurveyId = survey.Id;
                _checklistService.Update(checklist);
                _surveyRepository.Copy(survey);
                checklist.SurveyId = survey.Id;
                _checklistService.Update(checklist);
                _alertFactory.CreateSuccess(this, "Plantilla de Revisión de Unidad actualizada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = "Plantilla de Revisión de Unidad actualizada con éxito!",
                    Success = true
                });
            }
            catch (Exception e)
            {
                //   _alertFactory.CreateFailure(this, e.Message); 
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
                _checklistService.Delete(id);
                return _jsonFactory.Success("Template Revisión de Unidad eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion
    }
}
