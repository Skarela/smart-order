namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class ChecklistReply
    {
        public ChecklistReply()
        {
            Id = 0;
            ApplyAssignedSurveyId = 0;
            UnitId = 0;
            RouteId = 0;
            UserId = 0;
            GoodCondition = true;
            CreationDate = "";
        }

        public int Id { get; set; }
        public int ApplyAssignedSurveyId { get; set; }
        public int UnitId { get; set; }
        public int RouteId { get; set; }
        public int UserId { get; set; }
        public bool GoodCondition { get; set; }
        public string CreationDate { get; set; }
    }
}