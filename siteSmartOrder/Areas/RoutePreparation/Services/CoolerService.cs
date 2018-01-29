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
    public class CoolerService : ICoolerService
    {
        private IClient _client;

        public Cooler Get(string id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("coolers/{0}", id);
            try
            {
                return _client.Get<Cooler>(uri);
            }
            catch (Exception)
            {
                return new Cooler() { DoorsNumber = 0, Id = "0", Name = "NotFoundId:" + id, Serie = "0" };
            }
        }

        public CoolerPage Filter(CoolerFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("coolers");
            return _client.Filter<CoolerPage>(uri, request);
        }
    }
}