using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class NewCoolerMultimediaService : INewCoolerMultimediaService
    {
        private IClient _client;

        public NewCoolerMultimediaPage Filter (NewCoolerMultimediaFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("newcooler-multimedias");
            return _client.Filter<NewCoolerMultimediaPage>(uri, request);
        }
    }
}