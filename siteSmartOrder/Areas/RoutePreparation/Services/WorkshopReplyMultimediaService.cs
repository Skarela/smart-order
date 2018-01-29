using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class WorkshopReplyMultimediaService : IWorkshopReplyMultimediaService
    {
        private IClient _client;

        public WorkshopReplyMultimediaPage Filter(WorkshopReplyMultimediaFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("workshopreply-multimedias");
            return _client.Filter<WorkshopReplyMultimediaPage>(uri, request);
        }
    }
}