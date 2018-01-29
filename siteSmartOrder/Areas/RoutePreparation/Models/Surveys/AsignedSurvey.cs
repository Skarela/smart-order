namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class AssignedSurvey
    {
        public AssignedSurvey()
        {
            Id = 0;
            Survey = new Survey();
        }

        public int Id { get; set; }
        public Survey Survey { get; set; }
    }
}