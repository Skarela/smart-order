namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class AssignedSurveyFlat
    {
        public AssignedSurveyFlat()
        {
            Id = 0;
            Survey = new SurveyFlat();
        }

        public int Id { get; set; }
        public SurveyFlat Survey { get; set; }
    }
}