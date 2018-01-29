using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages
{
    public class ApplyAssignedSurveyPage
    {
        public ApplyAssignedSurveyPage()
        {
            ApplyAssignedSurveys = new List<ApplyAssignedSurvey>();
            ApplyAssignedSurveysTotal = 0;
        }


        public List<ApplyAssignedSurvey> ApplyAssignedSurveys { get; set; }
        public int ApplyAssignedSurveysTotal { get; set; }
    }
}