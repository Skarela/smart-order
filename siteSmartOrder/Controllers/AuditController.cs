using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RestSharp;
using siteSmartOrder.Models;
using siteSmartOrder.Models.Audit;
using siteSmartOrder.Util;

namespace siteSmartOrder.Controllers
{
    public class AuditController : Controller
    {
        private readonly RestClient _restClientToAudit;
        private readonly RestClient _restClientToPortal;

        public AuditController()
        {
            var auditServer = new Uri(ConfigurationManager.AppSettings["AssetControlServer"]);
            var portalServer = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            _restClientToAudit = new RestClient { BaseUrl = auditServer };
            _restClientToPortal = new RestClient { BaseUrl = portalServer };
        }

        #region Views

        [AuthorizeCustom]
        public ActionResult Index()
        {
            var branches = GetBranchesByUser();

            return View(branches);
        }

        [AuthorizeCustom]
        public ActionResult Create()
        {
            var branches = GetBranchesByUser();

            var auditCampaign = new CreateAuditCampaign
            {
                StartDate = "",
                EndDate = "",
                Name = "",
                Branches = branches
            };
            return View(auditCampaign);
        }

        [AuthorizeCustom]
        public ActionResult Extend(int id)
        {
            try
            {
                var auditCampaign = GetAuditCampaign(id);

                var extendAuditCampaign = new ExtendAuditCampaign
                {
                    Id = auditCampaign.Id,
                    Name = auditCampaign.Name,
                    StartDate = auditCampaign.StartDate,
                    EndDate = auditCampaign.EndDate
                };

                return View(extendAuditCampaign);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [AuthorizeCustom]
        public ActionResult Users(int id, string branchCode)
        {
            try
            {
                var auditCampaign = GetAuditCampaign(id);
                auditCampaign.BranchCode = branchCode;
                return View(auditCampaign);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [AuthorizeCustom]
        public ActionResult Audits(int auditCampaignId, int userId, string branchCode, string startDate, string endDate, int? statusAuditCampaign)
        {
            try
            {
                var user = GetUser(userId);

                var status = statusAuditCampaign.HasValue ? statusAuditCampaign.Value :
                    JTableAuditModel.Convert(startDate) > DateTime.Now.Date ? 0 : //sin iniciar
                       DateTime.Now.Date > JTableAuditModel.Convert(endDate) ? 2 : //finalizado
                           1; //En progreso


                var auditsContainer = new AuditsContainer
                {
                    User = user,
                    AuditsByCustomer = GetAuditsByUser(auditCampaignId, userId),
                    Pendings = GetAuditsByUser(user.Code, branchCode).Customers,
                    auditCampaignId = auditCampaignId,
                    userId = userId,
                    branchCode = branchCode,
                    statusAuditCampaign = status
                };
                return View(auditsContainer);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }

            return RedirectToAction("Users", new { id = userId });
        }

        #endregion

        #region Gets

        [AuthorizeCustom]
        public JsonResult AuditCampaigns(JTableAuditModel jtableAudit, int jtStartIndex = 0, int jtPageSize = 10,
            string jtSorting = null)
        {
            try
            {
                var campaignContainer = FindAuditsByPaginate(jtableAudit.BranchCode, jtableAudit.Name, jtableAudit.Date,
                    jtSorting, jtStartIndex, jtPageSize);
                var listJTable = jtableAudit.ConvertModelToRecords(campaignContainer.AuditCampaigns);

                return Json(new { Result = "OK", Records = listJTable, TotalRecordCount = campaignContainer.CountRows });
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR", Message = "Error al intentar recuperar auditorías!" });
            }
        }

        [AuthorizeCustom]
        public JsonResult GetUsersByBranch(string branchId)
        {
            try
            {
                var users = FindUsersByBranch(branchId);
                return Json(new { Result = "OK", Records = users });
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR", Message = "Error al intentar recuperar empleados!" });
            }
        }

        [AuthorizeCustom]
        public JsonResult GetUsersByAuditCampaign(JTableUserModel jtableUser, int jtStartIndex = 0, int jtPageSize = 10,
            string jtSorting = null)
        {
            try
            {
                var userContainer = GetUsersByAuditCampaign(jtableUser.AuditCampaignId, jtableUser.User, jtSorting,
                    jtStartIndex, jtPageSize);
                foreach (var user in userContainer.Users)
                {

                    user.StatusColumn =
                        JTableAuditModel.Convert(jtableUser.StartDate) > DateTime.Now.Date
                            ? Constants.StatusUnStartedColumn
                            : DateTime.Now.Date > JTableAuditModel.Convert(jtableUser.EndDate)
                                ? Constants.StatusFinalizedColumn
                                : CreateStatusBar(user.Id, jtableUser.AuditCampaignId, user.Code, jtableUser.BranchCode);
                }

                return
                    Json(new { Result = "OK", Records = userContainer.Users, TotalRecordCount = userContainer.CountRows });
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR", Message = "Error al intentar recuperar empleados!" });
            }
        }

        #endregion

        #region Create

        [AuthorizeCustom]
        [HttpPost]
        public ActionResult Create(CreateAuditCampaign auditCampaign)
        {
            try
            {
                var response = CreateAuditCampaign(auditCampaign);
                if (response.UsersToHaveActiveCampaign == null || !response.UsersToHaveActiveCampaign.Any())
                    TempData["Success"] = "Campaña creada con éxito";
                else
                    TempData["Error"] = "Los siguientes usuarios ya cuentan con una campaña activa: " +
                                        string.Join(",", response.UsersToHaveActiveCampaign);

                return auditCampaign.ViewCreate ? RedirectToAction("Create") : RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["Error"] = exception.Message;
            }

            auditCampaign.Branches = GetBranchesByUser();
            return View(auditCampaign);
        }

        #endregion

        #region Extend

        [AuthorizeCustom]
        [HttpPost]
        public ActionResult Extend(string id, string newDate)
        {
            try
            {
                ExtendAudit(Convert.ToInt32(id), newDate);
                TempData["Success"] = "Vigencia de campaña modificada con éxito!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return RedirectToAction("Extend", new { id });
        }

        #endregion

        #region Reassign

        [AuthorizeCustom]
        public ActionResult Reassign(string auditId, int auditCampaignId, int userId, string branchCode, int statusAuditCampaign)
        {
            try
            {
                ReassignAudit(Convert.ToInt32(auditId));
                TempData["Success"] = "Auditoria reasignada con éxito!";
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return RedirectToAction("Audits", new { auditCampaignId, userId, branchCode, statusAuditCampaign });
        }

        #endregion

        #region Private rutines

        private AuditCampaignContainer FindAuditsByPaginate(string branchCode, string name, string date, string sorting,
            int startIndex, int pageSize)
        {
            const string uri = "auditcampaign";

            var restRequest = new RestRequest(uri, Method.GET);

            if (!String.IsNullOrEmpty(branchCode))
                restRequest.AddParameter("BranchCode", branchCode);

            if (!String.IsNullOrEmpty(name))
                restRequest.AddParameter("Name", name);

            if (!String.IsNullOrEmpty(date))
                restRequest.AddParameter("Date", date.Replace("/", "-"));

            if (!String.IsNullOrEmpty(date))
                restRequest.AddParameter("Date", date.Replace("/", "-"));

            restRequest.AddParameter("SortBy", sorting.Split(' ')[0]);
            restRequest.AddParameter("Ascending", sorting.Split(' ')[1] == "ASC");
            restRequest.AddParameter("StartIndex", startIndex);
            restRequest.AddParameter("PageSize", pageSize);


            return ExcecuteRestClientAudit<AuditCampaignContainer>(restRequest);
        }

        private UserToAuditCampaignContainer GetUsersByAuditCampaign(int id, string user, string sorting,
            int startIndex, int pageSize)
        {
            var uri = String.Format("auditcampaign/{0}/users", id);
            var restRequest = new RestRequest(uri, Method.GET);

            if (!String.IsNullOrEmpty(user))
                restRequest.AddParameter("User", user);

            restRequest.AddParameter("SortBy", sorting.Split(' ')[0]);
            restRequest.AddParameter("Ascending", sorting.Split(' ')[1] == "ASC");
            restRequest.AddParameter("StartIndex", startIndex);
            restRequest.AddParameter("PageSize", pageSize);

            return ExcecuteRestClientAudit<UserToAuditCampaignContainer>(restRequest);
        }

        private List<AuditsByCustomer> GetAuditsByUser(int auditCampaignId, int userId)
        {
            var uri = String.Format("audit/auditcampaign/{0}/user/{1}", auditCampaignId, userId);
            var restRequest = new RestRequest(uri, Method.GET);
            return ExcecuteRestClientAudit<List<AuditsByCustomer>>(restRequest);
        }

        private AuditActiveCampaigns GetAuditsByUser(string userCode, string branchCode)
        {
            var uri = String.Format("auditcampaign/actives/usercode/{0}/branchcode/{1}", userCode, branchCode);
            var restRequest = new RestRequest(uri, Method.GET);
            return ExcecuteRestClientAudit<AuditActiveCampaigns>(restRequest);
        }

        private List<Branch> FindBranches(string code)
        {
            var request = new RestRequest("Branch/All", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { code });
            return ExcecuteRestClientPortal<List<Branch>>(request);
        }

        private List<UserToAudit> FindUsersByBranch(string branchId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            var request = new RestRequest("UserByBranch/Active", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { userPortal.code, branchId });
            var response = ExcecuteRestClientPortal<UserDataModel>(request);
            return response.Data.Data;
        }

        private List<Branch> GetBranchesByUser()
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            var branches = userPortal.branch == null
                ? FindBranches(userPortal.code)
                : new List<Branch>
                {
                    new Branch
                    {
                        branchId = userPortal.branch.branchId,
                        code = userPortal.branch.code,
                        name = userPortal.branch.name
                    }
                };
            return branches;
        }

        private AuditCampaign GetAuditCampaign(int id)
        {
            var uri = String.Format("auditcampaign/{0}", id);
            var request = new RestRequest(uri, Method.GET);

            return ExcecuteRestClientAudit<AuditCampaign>(request);
        }

        private UserToAuditCampaign GetUser(int id)
        {
            var uri = String.Format("user/{0}", id);
            var request = new RestRequest(uri, Method.GET);

            return ExcecuteRestClientAudit<UserToAuditCampaign>(request);
        }

        private CreateAuditCampaignResponse CreateAuditCampaign(CreateAuditCampaign auditCampaign)
        {
            const string uri = "auditcampaign";
            var json = new JavaScriptSerializer().Serialize(auditCampaign);

            var request = new RestRequest(uri, Method.POST);

            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            return ExcecuteRestClientAudit<CreateAuditCampaignResponse>(request);
        }

        private string CreateStatusBar(int userId, int auditCampaignId, string userCode, string branchCode)
        {
            var amountApplied = GetAuditsByUser(auditCampaignId, userId).Count;
            var amountPenddinng = GetAuditsByUser(userCode, branchCode).Count;
            var total = amountApplied + amountPenddinng;

            var percentDone = total == 0 ? 0 : (amountApplied / (float)(total)) * 100;
            var percentFail = 100 - percentDone;
            return BootstrapWbcHelper.StatusBar(percentDone, 0, percentFail, amountApplied, (total));
        }

        public SuccessResponse ExtendAudit(int id, string newDate)
        {
            const string uri = "auditcampaign/extend";
            var request = new RestRequest(uri, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(new { Id = id, EndDate = newDate });

            return ExcecuteRestClientAudit<SuccessResponse>(request);
        }

        public SuccessResponse ReassignAudit(int auditId)
        {
            const string uri = "audit/reassign";
            var request = new RestRequest(uri, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(new { Id = auditId, Status = 1 });

            return ExcecuteRestClientAudit<SuccessResponse>(request);
        }

        #endregion

        #region Client

        private TModelResponse ExcecuteRestClientAudit<TModelResponse>(IRestRequest restRequest)
        {
            restRequest.AddHeader("Content-Type", "application/json");
            var response = _restClientToAudit.Execute(restRequest);
            var jss = new JavaScriptSerializer();
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var errorResponse = jss.Deserialize<ErrorResponse>(response.Content);
                throw new Exception("Ocurrió un problema en el servidor de Auditorías. " + errorResponse.Message);
            }
            var responseContent = response.Content;
            var model = jss.Deserialize<TModelResponse>(responseContent);
            return model;
        }

        private TModelResponse ExcecuteRestClientPortal<TModelResponse>(IRestRequest restRequest)
        {
            restRequest.AddHeader("Content-Type", "application/json");
            var response = _restClientToPortal.Execute(restRequest);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception("Ocurrió un problema en el servidor de Auditorías");

            var jss = new JavaScriptSerializer();
            var responseContent = response.Content;
            var model = jss.Deserialize<TModelResponse>(responseContent);
            return model;
        }

        #endregion
    }
}