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
    public class AlertConfigurationService : IAlertConfigurationService
    {
        private IClient _client;

        public AlertConfiguration Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alert-Configurations/{0}", id);
            return _client.Get<AlertConfiguration>(uri);
        }

        public AlertConfigurationPage Filter(AlertConfigurationFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alert-Configurations");
            return _client.Filter<AlertConfigurationPage>(uri, request);
        }

        public void Create(AlertConfiguration alertConfiguration)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alert-Configurations");
            var response = _client.Post(uri, alertConfiguration);
            alertConfiguration.Id = response.Id;
        }

        public void Update(AlertConfiguration alertConfiguration)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alert-Configurations");
            var response = _client.Put(uri, alertConfiguration);
            alertConfiguration.Id = response.Id;
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("alert-Configurations/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }
    }
}