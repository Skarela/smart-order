using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Models.ViewModels;
using siteSmartOrder.Areas.RoutePreparation.Resolvers;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using System.Collections.Generic;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Mappers;
using System.Web.Script.Serialization;
using siteSmartOrder.Areas.RoutePreparation.Models.Dtos;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly ICampaignService _campaignService;
        private readonly ICampaignMultimediaService _campaignMultimediaService;
        private readonly IUserPortalService _userPortalService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;
        private readonly IBranchService _branchyService;

        public CampaignController(ISurveyService surveyRepository, ICampaignService campaignService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IUserPortalService userPortalService, ICampaignMultimediaService campaignMultimediaService, IBranchService branchyService)
        {
            _surveyRepository = surveyRepository;
            _campaignService = campaignService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _userPortalService = userPortalService;
            _campaignMultimediaService = campaignMultimediaService;
            _branchyService = branchyService;
        }

        #region Get Request

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
            CreateTemplateViewData();
            return View("New", new Campaign());
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult NewWithTemplate(int selectTemplates)
        {

            CreateTemplateViewData();

            var campaign = _campaignService.Get(selectTemplates);
            var campaignMultimediaResponse = _campaignMultimediaService.Filter(new CampaignMultimediaFilter { CampaignId = selectTemplates });
            campaign.CampaignMultimedias = campaignMultimediaResponse.CampaignMultimedias;
            campaign.CampaignMultimediaType = campaignMultimediaResponse.CampaignMultimedias.ResolverType();

            return View("New", campaign);
        }

        [HttpGet]
        [AuthorizeCustom]
        public PartialViewResult _AsignMultimediaTemplate(int campaignId)
        {
            try
            {
                var campaign = _campaignService.Get(campaignId);
                var campaignMultimediaResponse = _campaignMultimediaService.Filter(new CampaignMultimediaFilter { CampaignId = campaignId });
                campaign.CampaignMultimedias = campaignMultimediaResponse.CampaignMultimedias;
                campaign.CampaignMultimediaType = campaignMultimediaResponse.CampaignMultimedias.ResolverType();
                return PartialView("_Multimedia", campaign);
            }
            catch
            {
                return PartialView("Error");
            }
        }


        private void CreateTemplateViewData()
        {
            var listTemplates = new List<TemplateCampaignViewModel>();
            var templates = _campaignService.Filter(new CampaignFilter() { IsTemplate = true });

            foreach (var template in templates.Campaigns)
            {

                listTemplates.Add(new TemplateCampaignViewModel { Id = template.Id, Name = template.Name, SurveyId = template.SurveyId });
            }
            ViewData["Templates"] = listTemplates;
            ViewData["ControllerName"] = "Campaign";

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
                return RedirectToAction("Index", "Campaign");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var campaign = _campaignService.Get(id);
                return _jsonFactory.Success(campaign);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(CampaignFilter campaignFilter)
        {
            try
            {
                var response = _campaignService.Filter(campaignFilter);
                return _jsonFactory.Success(response.Campaigns, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(CampaignFilter campaignFilter)
        {
            try
            {
                var responseCampaigns = _campaignService.Filter(campaignFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "ENCUESTA,DESCRIPCIÓN,FECHA  INICIO,FECHA FIN, AUTOR");

                excel = (from campaign in responseCampaigns.Campaigns
                         let survey = _surveyRepository.GetFlat(campaign.SurveyId)
                         let userPortal = _userPortalService.Get(campaign.UserPortalId)
                         select survey.Name + "," + campaign.Description + "," + campaign.StartDate + "," + campaign.EndDate + "," + userPortal.Name).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Campañas " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Post Request

        private bool TryCreateSingle(Campaign campaign, Survey survey, int branch, List<Branch> branchList, out string branchName, string StartDate, string EndDate, out string message)
        {
            var surveyMapper = new SurveyMapper();
            branchName = branchList.First(x => x.Id == branch).Name;
            try
            {
                survey.BranchId = branch;
                var surveyCopy = surveyMapper.Clone(survey);
                _surveyRepository.Create(surveyCopy);
                campaign.SurveyId = surveyCopy.Id;
                campaign.BranchId = branch;
                campaign.StartDate = StartDate;
                campaign.EndDate = EndDate;
                var campaignCopy = CloneNew(campaign);
                _campaignService.Create(campaignCopy);
                campaign.CampaignMultimedias = campaignCopy.CampaignMultimedias;
                campaign.Files = new List<System.Web.HttpPostedFileBase>();
                message = "";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;

            }

        }
        
       

        [HttpPost]
        public JsonResult Create(Campaign campaign, Survey survey, List<int> BranchesList, string StartDate, string EndDate)
        {
            try
            {
                if (BranchesList.IsNull() || BranchesList.Count == 0)
                {
                    throw new Exception("La sucursal es requerida");
                }
                var errors = new List<error>();

                if (BranchesList.Any())
                {
                    var fullBranchList = _branchyService.Filter(new BranchFilter()).Branches;

                    survey.Category.Id = (int)CategoryType.Campaign;
                    survey.ShowPoints = true;
                    var branchName = "";
                    if (!errors.Any())
                    {
                        var message = "";
                        foreach (var branch in BranchesList)
                        {
                            if (!TryCreateSingle(campaign, survey, branch, fullBranchList, out branchName, StartDate
                                , EndDate, out message))
                            {
                                errors.Add(new error { eMessage = message, eIdentifier = branchName });
                            }
                        }
                    }
                }
                if (errors.Any())
                {
                    return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = GroupErrorMessages(errors), Success = false });
                }

                _alertFactory.CreateSuccess(this, "Campaña de concientización creada con éxito!");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = "Campaña de concientización creada con éxito!", Success = true });

            }
            catch (Exception e)
            {
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Success = false, Message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult Update(Campaign campaign, Survey survey)
        {
            try
            {
                var message = "";

                if (ValidateUpdate(campaign, out message))
                {
                    _surveyRepository.Copy(survey);
                    campaign.SurveyId = survey.Id;
                    _campaignService.Update(campaign);
                    _alertFactory.CreateSuccess(this, "Campaña de concientización editada con éxito!");
                    return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Success = true, Message = "Campaña de concientización editada con éxito!" });
                }
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Message = message, Success = false });

            }
            catch (Exception e)
            {
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Success = false, Message = e.Message });
            }
        }
        public bool ValidateUpdate(Campaign campaign, out string message)
        {
            try
            {
                _campaignService.ValidateUpdate(campaign);
                message = "";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        [HttpPut]
        public JsonResult DatesBulkUpdate(DatesBulkUpdateModel model)
        {
            var StartDate = model.StartDate;
            var EndDate = model.EndDate;
            var campaignsList = model.CampaignList.OrderBy(x => x.surveyName).ThenBy(x => x.branchName);
            try
            {
                var errors = new List<error>();
                foreach (var campaign in campaignsList)
                {
                    try
                    {
                        _campaignService.DatesBulkUpdate(campaign.id, StartDate, EndDate);
                    }
                    catch (Exception ex)

                    {
                        errors.Add(new error { eMessage = ex.Message, eIdentifier = campaign.branchName+": "+campaign.surveyName });
                    }
                }
                if (errors.Any())
                {
                    _alertFactory.CreateFailure(this, GroupErrorMessages(errors));
                }
                else
                {
                    _alertFactory.CreateSuccess(this, "Campañas de concientización editadas con éxito!");
                }
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Success = true, Message = "" });
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, "Las Campañas se intentaron editar con fechas invalidas");
                return _jsonFactory.Success<SuccessMessage>(new SuccessMessage() { Success = true, Message = "" });
            }
        }


        private Campaign CloneNew(Campaign campaign)
        {
            var jss = new JavaScriptSerializer();
            var serialCampaign = jss.Serialize(campaign);
            var campaignCopy = jss.Deserialize<Campaign>(serialCampaign);
            campaignCopy.Files = campaign.Files;

            foreach (var image in campaignCopy.Files)
            {
                if (image != null)
                {
                    image.InputStream.Position = 0L;
                }
            }

            campaignCopy.CampaignMultimedias = campaign.CampaignMultimedias;
            campaignCopy.CampaignMultimedias.ForEach(multimedia =>
            {
                if (multimedia.State == CampaignMultimedia.Unchanged)
                {
                    multimedia.State = CampaignMultimedia.Added;
                }
                else if (multimedia.State == CampaignMultimedia.Removed)// only do this when creating a new campaign
                {
                    multimedia.State = CampaignMultimedia.Ignore;
                }
            });
            return campaignCopy;

        }

        #endregion

        #region Delete Request

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _campaignService.Delete(id);
                return _jsonFactory.Success("Campaña de consientización eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

        public PartialViewResult GetMultimedias()
        {
            var campaignMultimediaPage = _campaignMultimediaService.Filter(new CampaignMultimediaFilter());
            return PartialView("_MultimediaModal", campaignMultimediaPage);
        }

        [HttpDelete]
        public JsonResult DeleteMultimedia(int id)
        {
            try
            {
                _campaignMultimediaService.Delete(id);
                return _jsonFactory.Success("Multimedia eliminada con éxito!");
            }
            catch (Exception exception)
            {
                return _jsonFactory.Failure(exception.Message, exception.GetType());
            }
        }

        private string GroupErrorMessages(List<error> errors)
        {
            var errorMessage = "";
            var groupedBy = errors.GroupBy(
                    x => x.eMessage);

                    foreach (var item in groupedBy)
                    {   
                        errorMessage += item.First().eMessage + " :</br>";
                        foreach(var i in item)
                        {
                            errorMessage += "* " +i.eIdentifier + "</br>";
                        }
                    }
                    return errorMessage;
        }

         private class error
        {
            public string eMessage;
            public string eIdentifier;
        }
    }
}
