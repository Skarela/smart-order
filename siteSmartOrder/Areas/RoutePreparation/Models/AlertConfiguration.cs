namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class AlertConfiguration
    {
        public AlertConfiguration()
        {
            Id = 0;
            SurveyId = 0;
            QuestionId = 0;
            AnswerId = 0;
            AlertId = 0;
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int AlertId { get; set; }
    }
}