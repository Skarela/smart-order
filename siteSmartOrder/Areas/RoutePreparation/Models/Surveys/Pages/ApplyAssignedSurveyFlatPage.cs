using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages
{
    public class ApplyAssignedSurveyFlatPage
    {
        public ApplyAssignedSurveyFlatPage()
        {
            ApplyAssignedSurveys = new List<ApplyAssignedSurveyFlat>();
            ApplyAssignedSurveysTotal = 0;
        }


        public List<ApplyAssignedSurveyFlat> ApplyAssignedSurveys { get; set; }
        public int ApplyAssignedSurveysTotal { get; set; }
    }
}