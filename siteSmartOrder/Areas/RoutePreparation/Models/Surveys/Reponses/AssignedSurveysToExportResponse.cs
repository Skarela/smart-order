using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Reponses
{
    public class AssignedSurveysToExportResponse
    {
        public AssignedSurveysToExportResponse()
        {
            AssignedSurveysToExport = new List<AppliedSurveyResponse>();
        }

        public List<AppliedSurveyResponse> AssignedSurveysToExport { get; set; }
    }
}