namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Workshop
    {
        public Workshop()
        {
            Id = 0;
            SurveyId = 0;
            ApplyAssignedSurveyId = 0;
            BranchId = 0;
            UserPortalId = 0;
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int ApplyAssignedSurveyId { get; set; }
        public int BranchId { get; set; }
        public int UserPortalId { get; set; }
    }
}