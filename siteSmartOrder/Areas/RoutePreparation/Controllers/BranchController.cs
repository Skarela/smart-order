using System;
using System.Linq;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Sessions;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Infrastructure.Settings;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchyService;
        private readonly IJsonFactory _jsonFactory;

        public BranchController(IBranchService branchyService, IJsonFactory jsonFactory)
        {
            _branchyService = branchyService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var branch = _branchyService.Get(id);
                return _jsonFactory.Success(branch);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(BranchFilter branchFilter)
        {
            try
            {
                var branchResponse = _branchyService.Filter(branchFilter);
                return _jsonFactory.Success(branchResponse);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult SelectedBranchSession(int id)
        {
            try
            {
                if (SessionSettings.ExistsSessionBranch)
                {
                    SessionSettings.CreateBranchSession(_branchyService.Filter(new BranchFilter()));
                }
                else
                {
                    SessionSettings.CreateBranchSession(_branchyService.Filter(new BranchFilter()));
                    SessionSettings.SelectedBranchSession(SessionSettings.SessionBranch.Branches.FirstOrDefault().Id);
                }

                SessionSettings.SelectedBranchSession(id);
                return _jsonFactory.Success(SessionSettings.SessionBranch);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult FilterByUserOnSession()
        {
            try
            {
                var userSession = SessionSettings.SessionUserPortal;
                var branchFilter = userSession.branch.IsNotNull()
                    ? new BranchFilter {Code = userSession.branch.code}
                    : new BranchFilter();

                if (!SessionSettings.ExistsSessionBranch)
                    SessionSettings.CreateBranchSession(_branchyService.Filter(branchFilter));
                return _jsonFactory.Success(SessionSettings.SessionBranch);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
