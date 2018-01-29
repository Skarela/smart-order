using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IRouteService
    {
        Route Get(int id);
        RoutePage Filter(RouteFilter request);
        UserRoutePage FilterByUser(UserRouteFilter request);
    }
}