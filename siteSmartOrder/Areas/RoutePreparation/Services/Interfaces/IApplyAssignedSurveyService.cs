using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IApplyAssignedSurveyService
    {
        ApplyAssignedSurvey Get(int id);
        ApplyAssignedSurveyFlat GetFlat(int id);
    }
}