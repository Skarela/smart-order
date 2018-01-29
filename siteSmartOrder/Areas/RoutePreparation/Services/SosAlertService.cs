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
    public class SosAlertService : ISosAlertService
    {
        private IClient _client;

        public SosAlert Get(string id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/alerts{0}", id);
            return _client.Get<SosAlert>(uri);
        }

        public SosAlertPage Filter(SosAlertFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/alerts");
            return _client.Filter<SosAlertPage>(uri, request);
        }

        public MultimediaPage GetMultimedias(int alertId, EMultimediaType multimediaType)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/alerts/{0}/multimedia/type/{1}", alertId, multimediaType);
            return _client.Filter<MultimediaPage>(uri);
        }

        public void Starting(SosAlertComment alertsComment)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/alerts/{0}/starting", alertsComment.Id);
            _client.ChangeStatus(uri, alertsComment);
        }

        public void Finalizing(SosAlertComment alertsComment)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/alerts/{0}/finalizing", alertsComment.Id);
            _client.ChangeStatus(uri, alertsComment);
        }
    }
}