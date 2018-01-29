using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IAlertService
    {
        Alert Get(int id);
        AlertPage Filter(AlertFilter request);
        void Create(Alert alert);
        void Update(Alert alert);
        void Delete(int id);
    }
}