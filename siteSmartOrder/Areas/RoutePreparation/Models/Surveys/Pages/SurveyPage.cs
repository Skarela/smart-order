using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages
{
    public class SurveyPage
    {
        public SurveyPage()
        {
            Surveys = new List<Survey>();
            SurveysTotal = 0;
        }

        public List<Survey> Surveys { get; set; }
        public int SurveysTotal { get; set; }
    }
}