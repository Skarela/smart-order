using System.Collections.Generic;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Reponses;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IAssignedSurveyService
    {
        AssignedSurveysToExportResponse ExportByApplyAssignedSurveyIds(List<int> ids);
    }
}