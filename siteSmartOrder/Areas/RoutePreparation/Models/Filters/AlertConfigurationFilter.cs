namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class AlertConfigurationFilter : Filter
    {
        public AlertConfigurationFilter()
        {
            SurveyId = 0;
        }

        public int SurveyId { get; set; }
    }
}