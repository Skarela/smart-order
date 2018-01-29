using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages
{
    public class SurveyFlatPage
    {
        public SurveyFlatPage()
        {
            Surveys = new List<SurveyFlat>();
            SurveysTotal = 0;
        }

        public List<SurveyFlat> Surveys { get; set; }
        public int SurveysTotal { get; set; }
    }
}