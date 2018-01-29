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
    public class CoolerConfigurationReplyController : Controller
    {
        private readonly ICoolerConfigurationReplyService _coolerConfigurationReplyService;
        private readonly IApplyAssignedSurveyService _applyAssignedSurveyService;
        private readonly IAssignedSurveyService _assignedSurveyService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        private readonly ICoolerService _coolerService;
        private readonly INewCoolerService _newCoolerService;
        private readonly IUserService _userService;
        private readonly IJsonFactory _jsonFactory;

        public CoolerConfigurationReplyController(ICoolerConfigurationReplyService coolerConfigurationReplyService, IJsonFactory jsonFactory, IApplyAssignedSurveyService applyAssignedSurveyService, IUserService userService, ICoolerService coolerService, INewCoolerService newCoolerService, IAssignedSurveyService assignedSurveyService, IBranchService branchService, ICustomerService customerService)
        {
            _coolerConfigurationReplyService = coolerConfigurationReplyService;
            _jsonFactory = jsonFactory;
            _applyAssignedSurveyService = applyAssignedSurveyService;
            _userService = userService;
            _coolerService = coolerService;
            _newCoolerService = newCoolerService;
            _assignedSurveyService = assignedSurveyService;
            _branchService = branchService;
            _customerService = customerService;
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
        public ViewResult Detail(int applyAssignedSurveyId, int customerId)
        {
            var applyAssignedSurvey = _applyAssignedSurveyService.Get(applyAssignedSurveyId);
            var customer = _customerService.Get(customerId);
            TempData["CustomerName"] = customer.Name;
            return View("Detail", applyAssignedSurvey);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var coolerConfigurationReply = _coolerConfigurationReplyService.Get(id);
                return _jsonFactory.Success(coolerConfigurationReply);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(CoolerConfigurationReplyFilter coolerConfigurationReplyFilter)
        {
            try
            {
                var response = _coolerConfigurationReplyService.Filter(coolerConfigurationReplyFilter);
                 return _jsonFactory.Success(response.CoolerConfigurationReplies, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(CoolerConfigurationReplyFilter coolerConfigurationReplyFilter)
        {
            try
            {
                var responseCoolerConfigurationReplies = _coolerConfigurationReplyService.Filter(coolerConfigurationReplyFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,CLIENTE,ENFRIADOR,NUEVO ENFIRADOR,FECHA,EXISTE,CONTAMINADO,BUEN ESTADO");

                excel = (from coolerConfigurationReply in responseCoolerConfigurationReplies.CoolerConfigurationReplies
                         let customerName = coolerConfigurationReply.CustomerId.IsGreaterThanZero() ? _customerService.Get(coolerConfigurationReply.CustomerId).Name : ""
                         let user = _userService.Get(coolerConfigurationReply.UserId)
                         let coolerName =coolerConfigurationReply.CoolerId.IsNotNullOrEmpty() ? _coolerService.Get(coolerConfigurationReply.CoolerId).Name :""
                         let exists = coolerConfigurationReply.Exists ? "Si" : "No"
                         let contaminated =  !coolerConfigurationReply.Exists ? "-": coolerConfigurationReply.Contaminated ? "Si" : "No"
                         let goodCondition = !coolerConfigurationReply.Exists ? "-" : coolerConfigurationReply.GoodCondition ? "Si" : "No"
                         let newCoolerName = coolerConfigurationReply.NewCoolerId.IsGreaterThanZero() ? "Nuevo" : ""
                         select user.Name + "," + customerName + "," + coolerName + "," + newCoolerName + "," + coolerConfigurationReply.CreationDate + "," + exists + "," + contaminated + "," + goodCondition).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Resultado de Revisión de Enfriadores " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        public FileResult ExportReport(CoolerConfigurationReplyFilter coolerConfigurationReplyFilter)
        {
            try
            {
                var coolerConfigurationReplies = _coolerConfigurationReplyService.Filter(coolerConfigurationReplyFilter).CoolerConfigurationReplies;
                var applyAssignedSurveyIds = coolerConfigurationReplies.Where(x => x.ApplyAssignedSurveyId.IsGreaterThanZero()).Select(x => x.ApplyAssignedSurveyId).ToList();
                var assignedSurveysToExport = _assignedSurveyService.ExportByApplyAssignedSurveyIds(applyAssignedSurveyIds).AssignedSurveysToExport;


                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,SUCURSAL,CLIENTE,ENFRIADOR,EXISTE,NUEVO,ENCUESTA,PREGUNTA,RESPUESTA");

                excel = (from assignedSurveyToExport in assignedSurveysToExport
                         let coolerConfigurationReply = coolerConfigurationReplies.FirstOrDefault(coolerConfigurationReply => coolerConfigurationReply.ApplyAssignedSurveyId.IsEqualTo(assignedSurveyToExport.ApplyAssignedSurveyId))
                         let userName = coolerConfigurationReply.IsNotNull() ? _userService.Get(coolerConfigurationReply.UserId).Name : ""
                         let branchName = coolerConfigurationReply.IsNotNull() ? _branchService.Get(coolerConfigurationReplyFilter.BranchId).Name : ""
                         let customerName = coolerConfigurationReply.CustomerId.IsGreaterThanZero() ? _customerService.Get(coolerConfigurationReply.CustomerId).Name : ""
                         let coolerName = coolerConfigurationReply.CoolerId.IsNotNullOrEmpty() ? _coolerService.Get(coolerConfigurationReply.CoolerId).Name : ""
                         let exists = coolerConfigurationReply.Exists ? "Si" : "No"
                         let newCoolerName = coolerConfigurationReply.NewCoolerId.IsGreaterThanZero() ? _newCoolerService.Get(coolerConfigurationReply.NewCoolerId).Name : ""
                         select userName + "," + branchName + "," + customerName + "," + coolerName + "," + exists + "," + newCoolerName + "," + assignedSurveyToExport.Encuesta + "," + assignedSurveyToExport.Pregunta + "," + assignedSurveyToExport.Respuesta).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                        );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Reporte de Revisión de Enfriadores " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }


        #endregion

    }
}
