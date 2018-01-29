using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class CoolerConfigurationService : ICoolerConfigurationService
    {
        private IClient _client;

        public CoolerConfiguration Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("coolerconfigurations/{0}", id);
            return _client.Get<CoolerConfiguration>(uri);
        }

        public CoolerConfigurationPage Filter(CoolerConfigurationFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            //request.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : request.BranchId;
            var uri = String.Format("coolerconfigurations");
            return _client.Filter<CoolerConfigurationPage>(uri, request);
        }

        public void Create(CoolerConfiguration coolerConfiguration)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            coolerConfiguration.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : coolerConfiguration.UserPortalId;
            //coolerConfiguration.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : coolerConfiguration.BranchId;
            var uri = String.Format("coolerconfigurations");
            var response = _client.Post(uri, coolerConfiguration);
            coolerConfiguration.Id = response.Id;
        }

        public void Update(CoolerConfiguration coolerConfiguration)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            coolerConfiguration.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : coolerConfiguration.UserPortalId;
            //coolerConfiguration.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : coolerConfiguration.BranchId;
            var uri = String.Format("coolerconfigurations");
            var response = _client.Put(uri, coolerConfiguration);
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("coolerconfigurations/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }
    }
}