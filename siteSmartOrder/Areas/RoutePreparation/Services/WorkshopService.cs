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
    public class WorkshopService : IWorkshopService
    {
        private IClient _client;

        public Workshop Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("workshops/{0}", id);
            return _client.Get<Workshop>(uri);
        }

        public WorkshopPage Filter(WorkshopFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("workshops");
            return _client.Filter<WorkshopPage>(uri, request);
        }

        public void Create(Workshop workshop)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            workshop.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : workshop.UserPortalId;
            workshop.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : workshop.BranchId;
            var uri = String.Format("workshops");
            var response = _client.Post(uri, workshop);
            workshop.Id = response.Id;
        }

        public void Update(Workshop workshop)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            workshop.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : workshop.UserPortalId;
            workshop.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : workshop.BranchId;
            var uri = String.Format("workshops");
            _client.Put(uri, workshop);
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("workshops/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }
    }
}