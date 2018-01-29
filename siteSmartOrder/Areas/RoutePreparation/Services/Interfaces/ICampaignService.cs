using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ICampaignService
    {
        Campaign Get(int id);
        CampaignPage Filter(CampaignFilter request);
        void Create(Campaign campaign);
        void ValidateCreate(Campaign campaign);
        void ValidateUpdate(Campaign campaign);
        void Update(Campaign campaign);
        void Delete(int id);
        void DatesBulkUpdate(int CampaignId, string StartDate, string EndDate);
    }
}