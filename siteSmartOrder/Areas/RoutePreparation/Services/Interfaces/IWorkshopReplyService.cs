using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IWorkshopReplyService
    {
        WorkshopReply Get(int id);
        WorkshopReplyPage Filter(WorkshopReplyFilter request);
    }
}