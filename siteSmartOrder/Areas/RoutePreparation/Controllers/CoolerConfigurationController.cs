using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
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
    public class CoolerConfigurationController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly ICoolerConfigurationService _coolerConfigurationService;
        private readonly IBranchService _branchService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;
        private readonly IBranchService _branchyService;

        public CoolerConfigurationController(ISurveyService surveyRepository, ICoolerConfigurationService coolerConfigurationService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IBranchService branchService, IBranchService branchyService)
        {
            _surveyRepository = surveyRepository;
            _coolerConfigurationService = coolerConfigurationService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _branchService = branchService;
            _branchyService = branchyService;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult RedirectToView()
        {
            try
            {
                return RedirectToAction("Index", "CoolerConfiguration");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult New()
        {
            TemplateViewData();
            return View("New", new CoolerConfiguration());
        }
        [HttpGet]
        [AuthorizeCustom]
        public ViewResult NewWithTemplate(int selectTemplates)
        {
            var coolerConfiguration = new CoolerConfiguration();
            coolerConfiguration.SurveyId = selectTemplates;


            TemplateViewData();
            return View("New", coolerConfiguration);
        }
        private void TemplateViewData()
        {
            var listTemplates = new List<TemplateViewModel>();
            var templates = _coolerConfigurationService.Filter(new CoolerConfigurationFilter() { IsTemplate = true });

            foreach (var template in templates.CoolerConfigurations)
            {
                var survey = _surveyRepository.GetPlain(template.SurveyId);

                listTemplates.Add(new TemplateViewModel { Id = survey.Id, Name = survey.Name });
            }
            ViewData["Templates"] = listTemplates;
            ViewData["ControllerName"] = "CoolerConfiguration";

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
                return RedirectToAction("RedirectToView", "CoolerConfiguration");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var coolerConfiguration = _coolerConfigurationService.Get(id);
                return _jsonFactory.Success(coolerConfiguration);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(CoolerConfigurationFilter coolerConfigurationFilter)
        {
            try
            {
                var response = _coolerConfigurationService.Filter(coolerConfigurationFilter);
                return _jsonFactory.Success(response.CoolerConfigurations, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

        #region Post Request

        private bool TryCreateSingle(CoolerConfiguration coolerConfiguration, Survey survey, int branch, List<Branch> branchList, out string branchName)
        {
            var surveyMapper = new SurveyMapper();
            branchName = branchList.First(x => x.Id == branch).Name;
            try
            {

                var surveyCopy = surveyMapper.Clone(survey);
                _surveyRepository.Create(surveyCopy);
                coolerConfiguration.SurveyId = surveyCopy.Id;
                coolerConfiguration.BranchId = branch;
                var coolerConfigCopy = Clone(coolerConfiguration);
                var coolerConfigExist = _coolerConfigurationService.Filter(new CoolerConfigurationFilter() { BranchId = branch });
                if (coolerConfigExist != null && coolerConfigExist.CoolerConfigurations.Any())
                {
                    coolerConfigCopy.Id = coolerConfigExist.CoolerConfigurations.First().Id;
                    _coolerConfigurationService.Update(coolerConfigCopy);
                }
                else
                {
                    _coolerConfigurationService.Create(coolerConfigCopy);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }




        [HttpPost]
        public JsonResult Create(CoolerConfiguration coolerConfiguration, Survey survey, List<int> BranchesList)
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
                        if (!TryCreateSingle(coolerConfiguration, survey, branch, fullBranchList, out branchName))
                        {
                            branchesWithError.Add(branchName);
                        }
                    }
                }
                if (branchesWithError.Any())
                {
                    //_alertFactory.CreateFailure(this, "Las siguientes revisiones tuvieron errores: " + string.Join(", ", branchesWithError));
                    return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                    {
                        Message = "Las siguientes revisiones tuvieron errores: " + string.Join(", ", branchesWithError),
                        Success = false
                    });
                }
                else
                {
                     _alertFactory.CreateSuccess(this, "Revisiónes de Enfriadores creadas con éxito!");
                    return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                    {
                        Message = "Revisiónes de Enfriadores creadas con éxito!",
                        Success = true
                    });
                }
            }
            catch (Exception e)
            {
                // _alertFactory.CreateFailure(this, e.Message);
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
                _alertFactory.CreateSuccess(this, "Revisión de Enfriadores editada con éxito!"); 
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = "Revisión de Enfriadores editada con éxito!",
                    Success = true
                });
            }
            catch (Exception e)
            {
              //  _alertFactory.CreateFailure(this, e.Message); 
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage()
                {
                    Message = "e.Message!",
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
                return _jsonFactory.Success("Revisión de enfriadores eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

        private CoolerConfiguration Clone(CoolerConfiguration coolerConfiguration)
        {
            var jss = new JavaScriptSerializer();
            var serialCoolerConfig = jss.Serialize(coolerConfiguration);
            var coolerConfigCopy = jss.Deserialize<CoolerConfiguration>(serialCoolerConfig);
            return coolerConfigCopy;
        }
    }
}
