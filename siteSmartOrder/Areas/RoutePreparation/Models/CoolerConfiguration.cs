namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class CoolerConfiguration
    {
        public CoolerConfiguration()
        {
            Id = 0;
            CustomerId = 0;
            SurveyId = 0;
            UserPortalId = 0;
            BranchId = 0;
            IsTemplate = false;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SurveyId { get; set; }
        public int UserPortalId { get; set; }
        public int BranchId { get; set; }
        public bool IsTemplate { get; set; }
    }
}