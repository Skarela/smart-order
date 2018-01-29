using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ICoolerConfigurationReplyMultimediaService
    {
        CoolerConfigurationReplyMultimediaPage Filter(CoolerConfigurationReplyMultimediaFilter request);
    }
}