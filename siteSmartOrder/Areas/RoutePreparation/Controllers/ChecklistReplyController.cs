using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Infrastructure.Settings;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class ChecklistReplyController : Controller
    {
        private readonly IChecklistReplyService _checklistReplyService;
        private readonly IApplyAssignedSurveyService _applyAssignedSurveyService;
        private readonly IAssignedSurveyService _assignedSurveyService;
        private readonly IBranchService _branchService;
        private readonly IUnitService _unitService;
        private readonly IRouteService _routeService;
        private readonly IUserService _userService;
        private readonly IJsonFactory _jsonFactory;

        public ChecklistReplyController(IChecklistReplyService checklistReplyService, IJsonFactory jsonFactory, IUserService userService, IUnitService unitService, IRouteService routeService, IApplyAssignedSurveyService applyAssignedSurveyService, IAssignedSurveyService assignedSurveyService, IBranchService branchService)
        {
            _checklistReplyService = checklistReplyService;
            _jsonFactory = jsonFactory;
            _userService = userService;
            _unitService = unitService;
            _routeService = routeService;
            _applyAssignedSurveyService = applyAssignedSurveyService;
            _assignedSurveyService = assignedSurveyService;
            _branchService = branchService;
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
                var checklistReply = _checklistReplyService.Get(id);
                return _jsonFactory.Success(checklistReply);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(ChecklistReplyFilter checklistReplyFilter)
        {
            try
            {
                var response = _checklistReplyService.Filter(checklistReplyFilter);
                return _jsonFactory.Success(response.ChecklistReplies, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(ChecklistReplyFilter checklistReplyFilter)
        {
            try
            {
                var responseChecklistReplies = _checklistReplyService.Filter(checklistReplyFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,UNIDAD,RUTA,ENCUESTA,FECHA,CONDICIÓN");

                excel = (from checklistReply in responseChecklistReplies.ChecklistReplies
                         let applyAssignedSurvey = _applyAssignedSurveyService.GetFlat(checklistReply.ApplyAssignedSurveyId)
                         let user = _userService.Get(checklistReply.UserId)
                         let route = _routeService.Get(checklistReply.RouteId)
                         let unit = _unitService.Get(checklistReply.UnitId)
                         let goodCondition = checklistReply.GoodCondition ? "Buena" : "Mala"
                         select user.Name + "," + unit.Code + "," + route.Name + "," + applyAssignedSurvey.AssignedSurvey.Survey.Name + "," + checklistReply.CreationDate + "," + goodCondition).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Resultados de Revisiones de Unidad " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        public FileResult ExportReport(ChecklistReplyFilter checklistReplyFilter)
        {
            try
            {
                var checklistReplies = _checklistReplyService.Filter(checklistReplyFilter).ChecklistReplies;
                var applyAssignedSurveyIds = checklistReplies.Select(x => x.ApplyAssignedSurveyId).ToList();
                var assignedSurveysToExport = _assignedSurveyService.ExportByApplyAssignedSurveyIds(applyAssignedSurveyIds).AssignedSurveysToExport;

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,SUCURSAL,UNIDAD,RUTA,ENCUESTA,FECHA,PREGUNTA,RESPUESTA");

                excel = (from assignedSurveyToExport in assignedSurveysToExport
                         let checklistReply = checklistReplies.FirstOrDefault(checklistReply => checklistReply.ApplyAssignedSurveyId.IsEqualTo(assignedSurveyToExport.ApplyAssignedSurveyId))
                         let userName = checklistReply.IsNotNull() ? _userService.Get(checklistReply.UserId).Name : ""
                         let branchName = checklistReply.IsNotNull() ? _branchService.Get(checklistReplyFilter.BranchId).Name : ""
                         let unitName = checklistReply.IsNotNull() ? _unitService.Get(checklistReply.UnitId).Code : ""
                         let routeName = checklistReply.IsNotNull() ? _routeService.Get(checklistReply.RouteId).Name : ""
                         select userName + "," + branchName + "," + unitName + "," + routeName + "," + assignedSurveyToExport.Encuesta + "," + checklistReply.CreationDate + "," + assignedSurveyToExport.Pregunta + "," + assignedSurveyToExport.Respuesta).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                        );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Reporte de Revisiones de Unidad " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
