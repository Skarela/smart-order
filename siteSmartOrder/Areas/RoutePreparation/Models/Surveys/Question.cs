using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class Question
    {
        public Question()
        {
            Id = 0;
            QuestionNumber = 0;
            Text = "";
            Required = false;
            QuestionType = 0;
            QuestionValue = 0;
            Answers = new List<Answer>();
            QuestionImages = new List<QuestionImage>();
            BranchId = 0;
            CompanyId = 0;
            Code = null;
        }

        public int Id { get; set; }
        public int QuestionNumber { get; set; }
        public string Text { get; set; }
        public bool Required { get; set; }
        public int QuestionType { get; set; }
        public int AnswerRequiredNumber { get; set; }
        public int QuestionValue { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionImage> QuestionImages { get; set; }        
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string QuestionImagesJson { get; set; }

        [ScriptIgnore]
        public List<HttpPostedFileBase> FileImages { get; set; }
    }
}