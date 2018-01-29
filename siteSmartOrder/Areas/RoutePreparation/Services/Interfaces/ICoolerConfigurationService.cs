using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ICoolerConfigurationService
    {
        CoolerConfiguration Get(int id);
        CoolerConfigurationPage Filter(CoolerConfigurationFilter request);
        void Create(CoolerConfiguration coolerConfiguration);
        void Update(CoolerConfiguration coolerConfiguration);
        void Delete(int id);
    }
}