using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Models.ViewModels;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using System.Collections.Generic;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Mappers;
using System.Web.Script.Serialization;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly IChecklistService _checklistService;
        private readonly IBranchService _branchService;
        private readonly IUserPortalService _userPortalService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;
        private readonly IBranchService _branchyService;

        public ChecklistController(ISurveyService surveyRepository, IChecklistService checklistService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IBranchService branchService, IUserPortalService userPortalService, IBranchService branchyService)
        {
            _surveyRepository = surveyRepository;
            _checklistService = checklistService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _branchService = branchService;
            _userPortalService = userPortalService;
            _branchyService = branchyService;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult RedirectToView()
        {
            try
            {
                return RedirectToAction("Index", "CheckList");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("RedirectToView", "CheckList");
            }
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult New()
        {
            TemplateViewData();
            return View("New", new Checklist());
        }
        
        [HttpGet]
        [AuthorizeCustom]
        public ViewResult NewWithTemplate( int selectTemplates)
        {
            var checklist = new Checklist();
            checklist.SurveyId = selectTemplates;
            
           
            TemplateViewData();
            return View("New", checklist);
        }

        private void TemplateViewData()
        {
            var listTemplates = new List<TemplateViewModel>();
            var templates = _checklistService.Filter(new ChecklistFilter() { IsTemplate = true });

            foreach (var template in templates.CheckLists)
            {
                var survey = _surveyRepository.GetPlain(template.SurveyId);

                listTemplates.Add(new TemplateViewModel { Id = survey.Id, Name = survey.Name });
            }
            ViewData["Templates"] = listTemplates;
            ViewData["ControllerName"] = "Checklist";

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
                return RedirectToAction("RedirectToView", "CheckList");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var checklist = _checklistService.Get(id);
                return _jsonFactory.Success(checklist);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(ChecklistFilter checklistFilter)
        {
            try
            {
                var response = _checklistService.Filter(checklistFilter);
                return _jsonFactory.Success(response.CheckLists, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(ChecklistFilter checklistFilter)
        {
            try
            {
                var responseCheckLists = _checklistService.Filter(checklistFilter);
                var excel = string.Empty;
                excel = excel.ConcatRow(0, "SUCURSAL,ENCUESTA,AUTOR");

                excel = (from checklist in responseCheckLists.CheckLists
                         let survey = _surveyRepository.GetFlat(checklist.SurveyId)
                         let branch = _branchService.Get(checklist.BranchId)
                         let userPortal = _userPortalService.Get(checklist.UserPortalId)
                         select branch.Name + "," + survey.Name + "," + userPortal.Name).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Revisiones de Unidad " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Post Request
        private bool TryCreateSingle(Checklist checklist, Survey survey, int branch,List<Branch> branchList , out string branchName){
            var surveyMapper = new SurveyMapper();
            branchName = branchList.First(x => x.Id == branch).Name;
            try
            {

                var surveyCopy = surveyMapper.Clone(survey);
                _surveyRepository.Create(surveyCopy);
                checklist.SurveyId = surveyCopy.Id;
                checklist.BranchId = branch;
                var checklistCopy = Clone(checklist);
                var checkListFound = _checklistService.Filter(new ChecklistFilter() { BranchId = checklistCopy.BranchId});
                if (checkListFound != null && checkListFound.CheckLists.Any())
                {
                    checklistCopy.Id = checkListFound.CheckLists.First().Id;
                    _checklistService.Update(checklistCopy);
                }
                else
                {
                    _checklistService.Create(checklistCopy);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }




        [HttpPost]
        public JsonResult Create(Checklist checklist, Survey survey, List<int> BranchesList, string submitBtn, int selectTemplates)
        {
            try
            {
                if (BranchesList.IsNull() || BranchesList.Count == 0)
                {
                    throw new Exception("La sucursal es requerida");
                }
                var branchesWithError = new List<string>();
                if (BranchesList.Any())
                {
                    var fullBranchList = _branchyService.Filter(new BranchFilter()).Branches;

                    survey.Category.Id = (int)CategoryType.Campaign;
                    survey.ShowPoints = true;
                    var branchName = "";
                    foreach (var branch in BranchesList)
                    {
                        if (!TryCreateSingle(checklist, survey, branch, fullBranchList, out branchName))
                        {
                            branchesWithError.Add(branchName);
                        }
                    }
                }
                if (branchesWithError.Any())
                {
                    //       _alertFactory.CreateFailure(this, "Las siguientes revisiones tuvieron errores: " + string.Join(", ", branchesWithError));
                    return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = "Las siguientes revisiones tuvieron errores: " + string.Join(", ", branchesWithError), Success = false });
                }
                else
                {

                 _alertFactory.CreateSuccess(this, "Revisiónes de Unidad creada con éxito!");
                    return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = "Campaña de concientización creada con éxito!", Success = true });

                }
            }
           
            catch (Exception e)
            {
                //    _alertFactory.CreateFailure(this, e.Message);
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = e.Message, Success = false });
                
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
                _alertFactory.CreateSuccess(this, "Revisión de Unidad actualizada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = "Revisión de Unidad actualizada con éxito!", Success = true });

            }
            catch (Exception e)
            {
               // _alertFactory.CreateFailure(this, e.Message);
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = e.Message, Success = false });
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
                return _jsonFactory.Success("Revisión de Unidad eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

        private Checklist Clone(Checklist checklist)
        {
            var jss = new JavaScriptSerializer();
            var serialChecklist = jss.Serialize(checklist);
            var checklistCopy = jss.Deserialize<Checklist>(serialChecklist);
            return checklistCopy;
        }

    }
}
