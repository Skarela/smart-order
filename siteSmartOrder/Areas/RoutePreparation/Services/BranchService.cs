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
    public class BranchService : IBranchService
    {
        private IClient _client;

        public BranchService()
        {
            //_client = new Client(new RestClient{BaseUrl = AppSettings.ServerSurveyEngineApi});
        }

        public Branch Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("branches/{0}", id);
            try
            {
                return _client.Get<Branch>(uri);
            }
            catch (Exception)
            {

                return new Branch() {Code = "0", Id = 0, Name = "notFoundId:"+id};
            }
        }

        public BranchPage Filter(BranchFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("branches");
            return _client.Filter<BranchPage>(uri, request);
        }
    }
}