using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.NoticeRecharge.Interfaces;
using siteSmartOrder.Areas.NoticeRecharge.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;

namespace siteSmartOrder.Areas.NoticeRecharge.Repositories
{
    public class RouteRepository:IRouteRepository
    {
        #region IRouteRepository Members

        public List<Route> GetByBranch(int branchId)
        {
            List<Route> routes = new List<Route>();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/Route/GetByBranch/{branchId}/Type/{type}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("branchId", branchId, ParameterType.UrlSegment);
            request.AddParameter("type", 1, ParameterType.UrlSegment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                routes = JsonConvert.DeserializeObject<List<Route>>(content);
            }
            return routes;
        }

        public List<Route> GetByUser(int userId)
        {
            List<Route> routes = new List<Route>();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/Route/GetByUser/{userNoticeRechargeId}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("userNoticeRechargeId", userId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                routes = JsonConvert.DeserializeObject<List<Route>>(content);
            }
            return routes;
        }

        #endregion
    }
}