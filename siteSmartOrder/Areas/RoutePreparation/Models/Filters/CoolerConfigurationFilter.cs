namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CoolerConfigurationFilter : Filter
    {
        public CoolerConfigurationFilter()
        {
            UserPortalId = 0;
            BranchId = 0;
            SurveyId = 0;
        }

        public int UserPortalId { get; set; }
        public int BranchId { get; set; }
        public int SurveyId { get; set; }
        public bool IsTemplate { get; set; }
    }
}