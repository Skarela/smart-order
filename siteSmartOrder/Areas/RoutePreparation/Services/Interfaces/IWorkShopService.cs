using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IWorkshopService
    {
        Workshop Get(int id);
        WorkshopPage Filter(WorkshopFilter request);
        void Create(Workshop workshop);
        void Update(Workshop workshop);
        void Delete(int id);
    }
}