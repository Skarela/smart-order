namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Filters
{
    public class SurveyFilter
    {
        public SurveyFilter()
        {
            SurveyId = 0;
            SurveyNameFilter = "";
            CategoryIdFilter = 0;
            StatusFilter = 0;
            Sorting = "";
            PageSize = 0;
            PageSize = 0;
        }

        public int SurveyId { get; set; }
        public string SurveyNameFilter { get; set; }
        public int CategoryIdFilter { get; set; }
        public int StatusFilter { get; set; }
        public string Sorting { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}