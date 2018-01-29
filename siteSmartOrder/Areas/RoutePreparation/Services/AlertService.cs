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
    public class AlertService : IAlertService
    {
        private IClient _client;

        public Alert Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alerts/{0}", id);
            return _client.Get<Alert>(uri);
        }

        public AlertPage Filter(AlertFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alerts");
            return _client.Filter<AlertPage>(uri, request);
        }

        public void Create(Alert alert)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alerts");
            var response = _client.Post(uri, alert);
            alert.Id = response.Id;
        }

        public void Update(Alert alert)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alerts");
            var response = _client.Put(uri, alert);
            alert.Id = response.Id;
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alerts/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }
    }
}