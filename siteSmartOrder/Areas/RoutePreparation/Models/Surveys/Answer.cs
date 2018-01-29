using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class Answer
    {
        public Answer()
        {
            Id = 0;
            Text = "";
            AnswerResponse = "";
            ImageRequired = false;
            AnswerPoint = 0;
            AnswerImages =  new List<AnswerImage>();
            BranchId = 0;
            CompanyId = 0;
            Code = null;
            AlertId = 0;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string AnswerResponse { get; set; }
        public bool ImageRequired { get; set; }
        public int AnswerPoint { get; set; }
        public List<AnswerImage> AnswerImages { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public int AlertId { get; set; }
    }
}