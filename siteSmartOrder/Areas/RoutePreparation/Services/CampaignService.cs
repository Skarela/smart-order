using System;
using System.Linq;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;
using System.Collections.Generic;
using siteSmartOrder.Areas.RoutePreparation.Models.Dtos.Requests;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignMultimediaService _campaignMultimediaService;
        private IClient _client;

        public CampaignService(ICampaignMultimediaService campaignMultimediaService)
        {
            _campaignMultimediaService = campaignMultimediaService;
        }

        public Campaign Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaigns/{0}", id);
            return _client.Get<Campaign>(uri);
        }

        public CampaignPage Filter(CampaignFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            //request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("campaigns");
            return _client.Filter<CampaignPage>(uri, request);
        }

        public void Create(Campaign campaign)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            campaign.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : campaign.UserPortalId;
            //campaign.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : campaign.BranchId;
            var uri = String.Format("campaigns");
            var response = _client.Post(uri, campaign);
            campaign.Id = response.Id;

            UpdateMultimediaOfCampaign(campaign);
        }

        public void ValidateCreate(Campaign campaign)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            campaign.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : campaign.UserPortalId;
            //campaign.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : campaign.BranchId;
            var uri = String.Format("validatecampaigns");
            var response = _client.Post(uri, campaign);
        }

        private void UpdateMultimediaOfCampaign(Campaign campaign)
        {
            foreach (var multimedia in campaign.CampaignMultimedias.Where(model => model.State == CampaignMultimedia.Removed))
            {
                UnLinkMultimediaToCampaign(campaign.Id, multimedia.Id);
            }

            foreach (var multimedia in campaign.CampaignMultimedias.Where(model => model.State == CampaignMultimedia.Added))
            {
                LinkMultimediaToCampaign(campaign.Id, multimedia.Id);
            }

            foreach (var fileImage in campaign.Files.Where(fileImage => fileImage.IsNotNull()))
            {
                var multimedia = campaign.CampaignMultimedias
                    .FirstOrDefault(model =>
                        fileImage.FileName == model.FileUploadedName &&
                        model.State == CampaignMultimedia.Created);
                if (multimedia.IsNull())
                {
                    continue;
                }
                var group = multimedia.IsNotNull() ? multimedia.Group : null;
                var createdMultimedia = _campaignMultimediaService.Create(campaign.Id, campaign.CampaignMultimediaType, fileImage, group);
                LinkMultimediaToCampaign(campaign.Id, createdMultimedia.Id);
                multimedia.Id = createdMultimedia.Id;
                multimedia.State = CampaignMultimedia.Added;
            }
        }

        private void LinkMultimediaToCampaign(int campaignId, int multimediaId)
        {
            var linkMultimediaUrl = string.Format("campaigns/{0}/multimedias/{1}", campaignId, multimediaId);
            _client.Put<SuccessResponse>(linkMultimediaUrl);
        }

        private void UnLinkMultimediaToCampaign(int campaignId, int multimediaId)
        {
            var linkMultimediaUrl = string.Format("campaigns/{0}/multimedias/{1}", campaignId, multimediaId);
            _client.Delete<SuccessResponse>(linkMultimediaUrl);
        }

        public void Update(Campaign campaign)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            campaign.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : campaign.UserPortalId;
            //campaign.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : campaign.BranchId;
            var uri = String.Format("campaigns");
            _client.Put(uri, campaign);

            UpdateMultimediaOfCampaign(campaign);
        }
        public void ValidateUpdate(Campaign campaign)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            campaign.UserPortalId = SessionSettings.ExistsSessionUserPortal ? SessionSettings.SessionUserPortal.userPortalId : campaign.UserPortalId;
            //campaign.BranchId = SessionSettings.ExistsSessionBranch ? SessionSettings.SessionBranch.SelectedBranch : campaign.BranchId;
            var uri = String.Format("validatecampaigns");
            _client.Put(uri, campaign);
        }
        public void DatesBulkUpdate(int CampaignId, string StartDate, string EndDate)
        {

            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = string.Format("campaigns/dates");
            var request = createDatesBulkUpdateRequest(CampaignId, StartDate, EndDate);
            _client.Put(uri, request);
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("campaigns/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }

        #region PrivateMethods

        private CampaignDatesRequest createDatesBulkUpdateRequest(int CampaignId, string StartDate, string EndDate)
        {
            var request = new CampaignDatesRequest()
            {
                CampaignDate =
                    new CampaignDate { Id = CampaignId, StartDate = StartDate, EndDate = EndDate }
            };
            
            return request;
        }


        #endregion
    }
}