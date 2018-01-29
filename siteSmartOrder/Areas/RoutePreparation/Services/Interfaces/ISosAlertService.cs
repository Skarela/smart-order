using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ISosAlertService
    {
        SosAlert Get(string id);
        SosAlertPage Filter(SosAlertFilter request);
        MultimediaPage GetMultimedias(int alertId, EMultimediaType multimediaType);
        void Starting(SosAlertComment alertsComment);
        void Finalizing(SosAlertComment alertsComment);
    }
}