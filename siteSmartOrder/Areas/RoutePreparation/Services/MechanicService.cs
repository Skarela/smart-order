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
    public class MechanicService : IMechanicService
    {
        private IClient _client;

        public Mechanic Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("mechanics/{0}", id);
            try
            {
                return _client.Get<Mechanic>(uri);
            }
            catch (Exception)
            {
                return new Mechanic(){BranchId = 0,Code = "0",Id = 0,Name = "NotFoundId:"+id};
            }
        }

        public MechanicPage Filter(MechanicFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("mechanics");
            return _client.Filter<MechanicPage>(uri, request);
        }
    }
}