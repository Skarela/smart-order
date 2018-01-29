using System;
using System.Collections.Generic;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Reponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Requests;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class AssignedSurveyService : IAssignedSurveyService
    {
        private IClient _client;

        private AssignedSurveysToExportResponse ExportByApplyAssignedSurveyIdsNonSegmented(List<int> ids)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var request = new ApplyAssignedSurveyIdsRequest { ApplyAssignedSurveyIds = ids.Count.IsGreaterThanZero() ? string.Join(",", ids) :"0"};
            var uri = String.Format("assignedsurveys/exportByApplyAssignedSurveyIds");
            var response = _client.Filter<AssignedSurveysToExportResponse>(uri, request);
            return response;
        }

        public AssignedSurveysToExportResponse ExportByApplyAssignedSurveyIds(List<int> applyAssignedSurveyIds)
        {
            const int sizeOfSegments = 100;
            var list = new AssignedSurveysToExportResponse();
            var segmentedLists = applyAssignedSurveyIds.ChunkBy(sizeOfSegments);
            foreach (var segment in segmentedLists)
            {
                var response = ExportByApplyAssignedSurveyIdsNonSegmented(segment);
                list.AssignedSurveysToExport.AddRange(response.AssignedSurveysToExport);
            }
            return list;
        }

    }
}