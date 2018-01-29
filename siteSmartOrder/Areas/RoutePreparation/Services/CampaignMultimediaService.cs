using System;
using System.Web;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class CampaignMultimediaService : ICampaignMultimediaService
    {
        private IClient _client;

        public CampaignMultimediaService()
        {
            //_client = new Client(new RestClient{BaseUrl = AppSettings.ServerSurveyEngineApi});
        }

        public CampaignMultimedia Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias/{0}", id);
            return _client.Get<CampaignMultimedia>(uri);
        }

        public CampaignMultimediaPage Filter(CampaignMultimediaFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias");
            return _client.Filter<CampaignMultimediaPage>(uri, request);
        }

        public CampaignMultimedia Create(int campaignId, int multimediaType, HttpPostedFileBase fileImage, string group)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias");
            var campaignMultimedia = new CampaignMultimedia {
                CampaignId = campaignId, 
                MultimediaType = multimediaType,
                Group = group
            };
            var response = _client.Post(uri, campaignMultimedia);
            AddImage(response.Id, fileImage);
            return response;
        }

        public void Update(CampaignMultimedia campaignMultimedia)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias");
            var response = _client.Put(uri, campaignMultimedia);
            campaignMultimedia.Id = response.Id;
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias/{0}", id);
            _client.Delete<SuccessResponse>(uri);
            DeleteFile(id);
        }

        private void DeleteFile(int id) 
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias/{0}/file", id);
            _client.Delete<SuccessResponse>(uri);
        }

        private void AddImage(int id, HttpPostedFileBase fileImage)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaign-multimedias/{0}/file", id);
            _client.AddFileByPut<CampaignMultimedia>(uri, fileImage);
        }
    }
}