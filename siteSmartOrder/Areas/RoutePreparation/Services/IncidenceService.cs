using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class IncidenceService : IIncidenceService
    {
        private IClient _client;

        public IncidencePage Filter(IncidenceFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerIncidentApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("getIncidents");
            return _client.Filter<IncidencePage>(uri, request.CreateJsonRequest());
        }

    }
}