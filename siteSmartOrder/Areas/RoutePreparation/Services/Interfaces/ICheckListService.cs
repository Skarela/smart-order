using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IChecklistService
    {
        Checklist Get(int id);
        ChecklistPage Filter(ChecklistFilter request);
        void Create(Checklist checklist);
        void Update(Checklist checklist);
        void Delete(int id);
    }
}