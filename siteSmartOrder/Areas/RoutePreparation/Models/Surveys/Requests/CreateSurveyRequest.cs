using System.Collections.Generic;
using System.Linq;
using siteSmartOrder.Infrastructure.Extensions;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Requests
{
    public class CreateSurveyRequest : Survey
    {
        public CreateSurveyRequest()
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
    }
}