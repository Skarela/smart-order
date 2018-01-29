using System.Web;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface ICampaignMultimediaService
    {
        CampaignMultimedia Get(int id);
        CampaignMultimediaPage Filter(CampaignMultimediaFilter request);
        CampaignMultimedia Create(int campaignId, int multimediaType, HttpPostedFileBase fileImage, string group);
        void Update(CampaignMultimedia campaignMultimedia);
        void Delete(int id);
    }
}