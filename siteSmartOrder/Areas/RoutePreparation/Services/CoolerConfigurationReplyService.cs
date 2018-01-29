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
    public class CoolerConfigurationReplyService : ICoolerConfigurationReplyService
    {
        private IClient _client;

        public CoolerConfigurationReplyService()
        {
            //_client = new Client(new RestClient{BaseUrl = AppSettings.ServerSurveyEngineApi});
        }

        public CoolerConfigurationReply Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("coolerconfiguration-replies/{0}", id);
            return _client.Get<CoolerConfigurationReply>(uri);
        }

        public CoolerConfigurationReplyPage Filter(CoolerConfigurationReplyFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("coolerconfiguration-replies");
            return _client.Filter<CoolerConfigurationReplyPage>(uri, request);
        }
    }
}