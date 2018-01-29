using System;
using System.Linq;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class ApplyAssignedSurveyService : IApplyAssignedSurveyService
    {
        private IClient _client;

        public ApplyAssignedSurvey Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("applyassignedsurveys/{0}", id);
            var response = _client.Get<ApplyAssignedSurveyPage>(uri);
            return response.ApplyAssignedSurveys.FirstOrDefault();
        }

        public ApplyAssignedSurveyFlat GetFlat(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("applyassignedsurveys/{0}", id);
            var response = _client.Get<ApplyAssignedSurveyFlatPage>(uri);
            try
            {
                return response.ApplyAssignedSurveys.FirstOrDefault();
            }
            catch (Exception)
            {
                return new ApplyAssignedSurveyFlat
                {
                    AssignedSurvey =new AssignedSurveyFlat(){Id = 0, Survey = new SurveyFlat() {Id = 0, Name = "Not Found"}},
                    Id=0
                };
            }
        }
    }
}