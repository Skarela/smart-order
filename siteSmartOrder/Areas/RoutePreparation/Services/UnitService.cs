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
    public class UnitService : IUnitService
    {
        private IClient _client;

        public UnitService()
        {
            //_client = new Client(new RestClient{BaseUrl = AppSettings.ServerSurveyEngineApi});
        }

        public Unit Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("units/{0}", id);
            try
            {
                return _client.Get<Unit>(uri);
            }
            catch (Exception)
            {
                return new Unit {RouteId = 0, Code = "0", Id = 0};
            }
        }

        public UnitPage Filter(UnitFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("units");
            return _client.Filter<UnitPage>(uri, request);
        }
    }
}