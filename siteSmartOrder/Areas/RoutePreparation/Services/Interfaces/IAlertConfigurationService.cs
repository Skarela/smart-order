using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IAlertConfigurationService
    {
        AlertConfiguration Get(int id);
        AlertConfigurationPage Filter(AlertConfigurationFilter request);
        void Create(AlertConfiguration alertConfiguration);
        void Update(AlertConfiguration alertConfiguration);
        void Delete(int id);
    }
}