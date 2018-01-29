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
    public class NewCoolerService : INewCoolerService
    {
        private IClient _client;

        public NewCoolerService()
        {
            //_client = new Client(new RestClient{BaseUrl = AppSettings.ServerSurveyEngineApi});
        }

        public NewCooler Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("newcoolers/{0}", id);
            return _client.Get<NewCooler>(uri);
        }

        public NewCoolerPage Filter(NewCoolerFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("newcoolers");
            return _client.Filter<NewCoolerPage>(uri, request);
        }
    }
}