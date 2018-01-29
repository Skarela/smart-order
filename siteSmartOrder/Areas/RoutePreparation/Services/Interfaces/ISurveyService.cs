using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ISurveyService
    {
        Survey Get(int id);
        Survey GetLastActive(string code);
        SurveyFlat GetFlat(int id);
        Survey GetPlain(int id);
        //Survey GetLastActive(string code);
        SurveyPage Filter(SurveyFilter request);
        void Create(Survey survey);
        void Update(Survey survey);
        void Copy(Survey survey);
        void Delete(int id);
    }
}