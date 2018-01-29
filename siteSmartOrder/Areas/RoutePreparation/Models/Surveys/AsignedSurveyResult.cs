using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class AssignedSurveyResult
    {
        public AssignedSurveyResult()
        {
            Id = 0;
            AnswerPoint = 0;
            QuestionId = 0;
            AnswerId = 0;
            TextResult = "";
            Text = "";
            AssignedSurveyResultImages = new List<AssignedSurveyResultImage>();
        }

        public int Id { get; set; }
        public int AnswerPoint { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string TextResult { get; set; }
        public string Text { get; set; }
        public List<AssignedSurveyResultImage> AssignedSurveyResultImages { get; set; }
    }
}