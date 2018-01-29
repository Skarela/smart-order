using System.Collections.Generic;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IManagerService
    {
        Manager Get(int id);
        ManagerPage Filter(ManagerFilter request);
        List<Incident> GetIncidents(int id);
        void Create(Manager manager);
        void Update(Manager manager);
        void Delete(int id);
    }
}