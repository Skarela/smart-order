namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class ApplyAssignedSurveyFlat
    {
        public ApplyAssignedSurveyFlat()
        {
            Id = 0;
            AssignedSurvey = new AssignedSurveyFlat();
        }

        public int Id { get; set; }
        public AssignedSurveyFlat AssignedSurvey { get; set; }
    }
}