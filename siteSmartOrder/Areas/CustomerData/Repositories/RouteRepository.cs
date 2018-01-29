using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.CustomerData.Interfaces;
using siteSmartOrder.Areas.CustomerData.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;

namespace siteSmartOrder.Areas.CustomerData.Repositories
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
            var request = new RestRequest("api/Route/GetByUser/{userCustomerDataId}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("userCustomerDataId", userId, ParameterType.UrlSegment);
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