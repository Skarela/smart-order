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
    public class WorkshopReplyController : Controller
    {
        private readonly IApplyAssignedSurveyService _applyAssignedSurveyService;
        private readonly IWorkshopReplyService _workshopReplyService;
        private readonly IAssignedSurveyService _assignedSurveyService;
        private readonly IBranchService _branchService;
        private readonly IMechanicService _mechanicService;
        private readonly IUnitService _unitService;
        private readonly IJsonFactory _jsonFactory;

        public WorkshopReplyController(IWorkshopReplyService workshopReplyService, IJsonFactory jsonFactory, IUnitService unitService, IMechanicService mechanicService, IApplyAssignedSurveyService applyAssignedSurveyService, IAssignedSurveyService assignedSurveyService, IBranchService branchService)
        {
            _workshopReplyService = workshopReplyService;
            _jsonFactory = jsonFactory;
            _unitService = unitService;
            _mechanicService = mechanicService;
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
                var workshopReply = _workshopReplyService.Get(id);
                return _jsonFactory.Success(workshopReply);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(WorkshopReplyFilter workshopReplyFilter)
        {
            try
            {
                var response = _workshopReplyService.Filter(workshopReplyFilter);
                return _jsonFactory.Success(response.WorkshopReplies, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(WorkshopReplyFilter workshopReplyFilter)
        {
            try
            {
                var responseChecklistReplies = _workshopReplyService.Filter(workshopReplyFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "MECÁNICO,UNIDAD,ENCUESTA,FECHA");

                excel = (from workshopReply in responseChecklistReplies.WorkshopReplies
                         let applyAssignedSurvey = _applyAssignedSurveyService.GetFlat(workshopReply.ApplyAssignedSurveyId)
                         let unit = _unitService.Get(workshopReply.UnitId)
                         let mechanic = _mechanicService.Get(workshopReply.MechanicId)
                         select mechanic.Name + "," + unit.Code + "," + applyAssignedSurvey.AssignedSurvey.Survey.Name + "," + workshopReply.CreationDate).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Resultados de Revisiones de Taller " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        public FileResult ExportReport(WorkshopReplyFilter workshopReplyFilter)
        {
            try
            {
                var workshopReplies = _workshopReplyService.Filter(workshopReplyFilter).WorkshopReplies;
                var applyAssignedSurveyIds = workshopReplies.Select(x => x.ApplyAssignedSurveyId).ToList();
                var assignedSurveysToExport = _assignedSurveyService.ExportByApplyAssignedSurveyIds(applyAssignedSurveyIds).AssignedSurveysToExport;

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "MECÁNICO,SUCURSAL,UNIDAD,ENCUESTA,FECHA,PREGUNTA,RESPUESTA");

                excel = (from assignedSurveyToExport in assignedSurveysToExport
                         let workshopReply = workshopReplies.FirstOrDefault(workshopReply => workshopReply.ApplyAssignedSurveyId.IsEqualTo(assignedSurveyToExport.ApplyAssignedSurveyId))
                         let mechanicName = workshopReply.IsNotNull() ? _mechanicService.Get(workshopReply.MechanicId).Name : ""
                         let branchName = workshopReply.IsNotNull() ? _branchService.Get(SessionSettings.SessionBranch.SelectedBranch).Name : ""
                         let unitName = workshopReply.IsNotNull() ? _unitService.Get(workshopReply.UnitId).Code : ""
                         select mechanicName + "," + branchName + "," + unitName + "," + assignedSurveyToExport.Encuesta + "," + workshopReply.CreationDate + "," + assignedSurveyToExport.Pregunta + "," + assignedSurveyToExport.Respuesta).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                        );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Reporte de Revisiones de Taller " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
