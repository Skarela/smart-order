namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class WorkshopReply
    {
        public WorkshopReply()
        {
            Id = 0;
            ApplyAssignedSurveyId = 0;
            UnitId = 0;
            MechanicId = 0;
            CreationDate = "";
        }

        public int Id { get; set; }
        public int ApplyAssignedSurveyId { get; set; }
        public int UnitId { get; set; }
        public int MechanicId { get; set; }
        public string CreationDate { get; set; }
    }
}