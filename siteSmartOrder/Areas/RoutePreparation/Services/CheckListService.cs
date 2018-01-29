using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class ChecklistService : IChecklistService
    {
        private IClient _client;

        public Checklist Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("checklists/{0}", id);
            return _client.Get<Checklist>(uri);
        }

        public ChecklistPage Filter(ChecklistFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            //request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("checklists");
            var response = _client.Filter<ChecklistPage>(uri, request);
            return response;
        }

        public void Create(Checklist checklist)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            checklist.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : checklist.UserPortalId;
            //checklist.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : checklist.BranchId;
            var uri = String.Format("checklists");
            var response = _client.Post(uri, checklist);
            checklist.Id = response.Id;
        }

        public void Update(Checklist checklist)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            checklist.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : checklist.UserPortalId;
            //checklist.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : checklist.BranchId;
            var uri = String.Format("checklists");
            _client.Put(uri, checklist);
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("checklists/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }
    }
}