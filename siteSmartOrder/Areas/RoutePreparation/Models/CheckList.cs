namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Checklist
    {
        public Checklist()
        {
            Id = 0;
            SurveyId = 0;
            BranchId = 0;
            UserPortalId = 0;
            IsTemplate = false;
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int BranchId { get; set; }
        public int UserPortalId { get; set; }
        public bool IsTemplate { get; set; }
    }
}