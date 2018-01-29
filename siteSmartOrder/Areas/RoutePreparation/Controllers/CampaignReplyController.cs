using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Reponses;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Infrastructure.Settings;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CampaignReplyController : Controller
    {
        private readonly ICampaignReplyService _campaignReplyService;
        private readonly IApplyAssignedSurveyService _applyAssignedSurveyService;
        private readonly IAssignedSurveyService _assignedSurveyService;
        private readonly IRouteService _routeService;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;
        private readonly IJsonFactory _jsonFactory;

        public CampaignReplyController(ICampaignReplyService campaignReplyService, IJsonFactory jsonFactory, IUserService userService, IApplyAssignedSurveyService applyAssignedSurveyService, IAssignedSurveyService assignedSurveyService, IBranchService branchService, IRouteService routeService)
        {
            _campaignReplyService = campaignReplyService;
            _jsonFactory = jsonFactory;
            _userService = userService;
            _applyAssignedSurveyService = applyAssignedSurveyService;
            _assignedSurveyService = assignedSurveyService;
            _branchService = branchService;
            _routeService = routeService;
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
        public ViewResult Detail(int id)
        {
            var applyAssignedSurvey = _applyAssignedSurveyService.Get(id);
            return View("Detail", applyAssignedSurvey);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var campaignReply = _campaignReplyService.Get(id);
                return _jsonFactory.Success(campaignReply);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(CampaignReplyFilter campaignReplyFilter)
        {
            try
            {
                var response = _campaignReplyService.Filter(campaignReplyFilter);
                return _jsonFactory.Success(response.CampaignReplies, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(CampaignReplyFilter campaignReplyFilter)
        {
            try
            {
                var responseCampaignReplies = _campaignReplyService.Filter(campaignReplyFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,RUTA,ENCUESTA,FECHA");

                excel = (from campaignReply in responseCampaignReplies.CampaignReplies
                         let applyAssignedSurvey = _applyAssignedSurveyService.GetFlat(campaignReply.ApplyAssignedSurveyId)
                         let user = _userService.Get(campaignReply.UserId)
                         let route = _routeService.Get(campaignReply.RouteId)
                         select user.Name + "," + route.Name + "," + applyAssignedSurvey.AssignedSurvey.Survey.Name + "," + campaignReply.CreationDate).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Resultado de Campañas " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        public FileResult ExportReport(CampaignReplyFilter campaignReplyFilter)
        {
            try
            {
                var campaignReplies = _campaignReplyService.Filter(campaignReplyFilter).CampaignReplies;
                var applyAssignedSurveyIds = campaignReplies.Select(x => x.ApplyAssignedSurveyId).ToList();
                var assignedSurveysToExport = _assignedSurveyService.ExportByApplyAssignedSurveyIds(applyAssignedSurveyIds).AssignedSurveysToExport;

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,SUCURSAL,RUTA,ENCUESTA,FECHA,PREGUNTA,RESPUESTA");

                excel = (from assignedSurveyToExport in assignedSurveysToExport
                         let campaignReply = campaignReplies.FirstOrDefault(campaignReply => campaignReply.ApplyAssignedSurveyId.IsEqualTo(assignedSurveyToExport.ApplyAssignedSurveyId))
                         let userName = campaignReply.IsNotNull() ? _userService.Get(campaignReply.UserId).Name : ""
                         let branchName = campaignReply.IsNotNull() ? _branchService.Get(campaignReplyFilter.BranchId).Name : ""
                         let routeName = campaignReply.IsNotNull() ? _routeService.Get(campaignReply.RouteId).Name : ""
                         select userName + "," + branchName + "," + routeName + "," + assignedSurveyToExport.Encuesta + "," + campaignReply.CreationDate + "," + assignedSurveyToExport.Pregunta + "," + assignedSurveyToExport.Respuesta).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                        );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Reporte de Campañas " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }


        #endregion

    }
}
