using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class CampaignReplyService : ICampaignReplyService
    {
        private IClient _client;

        public CampaignReply Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-replies/{0}", id);
            return _client.Get<CampaignReply>(uri);
        }

        public CampaignReplyPage Filter(CampaignReplyFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("campaign-replies");
            return _client.Filter<CampaignReplyPage>(uri, request);
        }
    }
}