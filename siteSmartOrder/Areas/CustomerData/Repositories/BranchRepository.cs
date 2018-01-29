using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.CustomerData.Interfaces;
using siteSmartOrder.Areas.CustomerData.Models;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace siteSmartOrder.Areas.CustomerData.Repositories
{
    public class BranchRepository:IBranchRepository
    {

        #region IBranchRepository Members

        public List<Branch> Get()
        {
            var branches = new List<Branch>();
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/Branch", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                branches = JsonConvert.DeserializeObject<List<Branch>>(content);
            }
            return branches;
        }

        #endregion
    }
}