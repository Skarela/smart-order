using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class ApplyAssignedSurvey
    {
        public ApplyAssignedSurvey()
        {
            Id = 0;
            ClientDate = "";
            AssignedSurvey = new AssignedSurvey();
            AssignedSurveyResults = new List<AssignedSurveyResult>();
        }

        public int Id { get; set; }
        public string ClientDate { get; set; }
        public AssignedSurvey AssignedSurvey { get; set; }
        public List<AssignedSurveyResult> AssignedSurveyResults { get; set; }
    }
}