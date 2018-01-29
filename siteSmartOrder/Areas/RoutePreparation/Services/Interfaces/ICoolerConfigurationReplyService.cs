using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ICoolerConfigurationReplyService
    {
        CoolerConfigurationReply Get(int id);
        CoolerConfigurationReplyPage Filter(CoolerConfigurationReplyFilter request);
    }
}