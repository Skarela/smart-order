namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Reponses
{
    public class AppliedSurveyResponse
    {
        public AppliedSurveyResponse()
        {
            Encuesta = "";
            Pregunta = "";
            Respuesta = "";
            Calificacion = "";
            ApplyAssignedSurveyId = 0;
        }

        public string Encuesta { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public string Calificacion { get; set; }
        public int ApplyAssignedSurveyId { get; set; }
    }
}