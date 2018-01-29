using System.Linq;
using siteSmartOrder.Infrastructure.Extensions;
using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class Survey
    {
        public Survey()
        {
            SurveyId = 0;
            Name = "";
            Description = "";
            Status = 0;
            ShowPoints = false;
            Category = new Category();
            Questions = new List<Question>();
            BranchId = 0;
            CompanyId = 0;
            Code = null;
        }

        public int Id { get; set; }
        public int SurveyId
        {
            get { return Id; }
            set { Id = value; }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public bool Weighted
        {
            get { return ShowPoints || Questions.Any(q => q.QuestionValue.IsGreaterThanZero()); }
        }
        public bool ShowPoints { get; set; }
        public Category Category { get; set; }
        public List<Question> Questions { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
    }
}