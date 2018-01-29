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
    public class RouteService : IRouteService
    {
        private IClient _client;

        public Route Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("routes/{0}", id);
            try
            {
                return _client.Get<Route>(uri);
            }
            catch (Exception)
            {

                return new Route { BranchId = 0, Code = "0", Id = 0, Name = "notFoundId" + id };
            }
        }

        public RoutePage Filter(RouteFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("routes");
            return _client.Filter<RoutePage>(uri, request);
        }

        public UserRoutePage FilterByUser(UserRouteFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : request.BranchId;
            var uri = String.Format("routes");
            return _client.Filter<UserRoutePage>(uri, request);
        }
    }
}