using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class ManagerService : IManagerService
    {
        private IClient _client;

        public Manager Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/{0}", id);
            return _client.Get<Manager>(uri);
        }

        public ManagerPage Filter(ManagerFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/");
            return _client.Filter<ManagerPage>(uri, request);
        }

        public List<Incident> GetIncidents(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/{0}", id);
            var response = _client.Get<Manager>(uri);
            return response.Incidents.ToList();
        }

        public void Create(Manager manager)
        {
            _client = new Client(new RestClient {BaseUrl = AppSettings.ServerSurveyEngineApi});
            var uri = String.Format("sos/managers/");
            var response = _client.Post(uri, manager);
            manager.Id = response.Id;

            if (manager.File.IsNotNull())
                AddImage(manager.Id, manager.File);

            if (manager.Branches.IsNotEmpty())
            {
                foreach (var branch in manager.Branches)
                    AssignBranch(manager.Id, branch.Id);
            }
        }

        public void Update(Manager manager)
        {
            _client = new Client(new RestClient {BaseUrl = AppSettings.ServerSurveyEngineApi});
            var uri = String.Format("sos/managers/{0}", manager.Id);
            _client.Put(uri, manager);

            if (manager.File.IsNotNull())
                AddImage(manager.Id, manager.File);

            ResolveRelationBranches(manager);
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }

        private void AddImage(int id, HttpPostedFileBase fileImage)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/{0}/image", id);
            _client.AddFileByPost<FileResponse>(uri, fileImage);
        }

        private void ResolveRelationBranches(Manager manager)
        {
            var assignedBranches = Get(manager.Id).Branches;

            foreach (var branch in manager.Branches)
            {
                var currentBranch = assignedBranches.FirstOrDefault(x => x.Id.IsEqualTo(branch.Id));
                if (currentBranch.IsNull())
                    AssignBranch(manager.Id, branch.Id);
            }

            foreach (var assignedBranch in assignedBranches)
            {
                var currentBranch = manager.Branches.FirstOrDefault(x => x.Id.IsEqualTo(assignedBranch.Id));
                if (currentBranch.IsNull())
                    UnassignBranch(manager.Id, assignedBranch.Id);
            }
        }

        private void AssignBranch(int managerId, int branchId)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/{0}/branches/{1}", managerId, branchId);
            _client.Put<SuccessResponse>(uri);
        }

        private void UnassignBranch(int managerId, int branchId)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("sos/managers/{0}/branches/{1}", managerId, branchId);
            _client.Delete<SuccessResponse>(uri);
        }
    }
}