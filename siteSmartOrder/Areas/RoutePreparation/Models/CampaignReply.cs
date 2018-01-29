namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class CampaignReply
    {
        public CampaignReply()
        {
            Id = 0;
            RouteId = 0;
            ApplyAssignedSurveyId = 0;
            UserId = 0;
            CampaignId = 0;
            CreationDate = "";
        }

        public int Id { get; set; }
        public int RouteId { get; set; }
        public int ApplyAssignedSurveyId { get; set; }
        public int UserId { get; set; }
        public int CampaignId { get; set; }
        public string CreationDate { get; set; }
    }
}