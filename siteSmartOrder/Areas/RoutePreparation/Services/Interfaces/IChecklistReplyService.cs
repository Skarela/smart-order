using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IChecklistReplyService
    {
        ChecklistReply Get(int id);
        ChecklistReplyPage Filter(ChecklistReplyFilter request);
    }
}