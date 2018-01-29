using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.CustomerData.Interfaces;
using siteSmartOrder.Areas.CustomerData.Models;
using RestSharp;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace siteSmartOrder.Areas.CustomerData.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        #region ICustomerRepository Members

        public List<Customer> Get(int routeId)
        {
            List<Customer> customers = new List<Customer>();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/Customer/Data/GetByRoute/{RouteId}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("RouteId", routeId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                customers = JsonConvert.DeserializeObject<List<Customer>>(content);
            }

            return customers;
        }
        
        public ChangeStatusResponse Set(Customer customer)
        {


            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/Customer/Data/Status/{CustomerId}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("CustomerId", customer.Id, ParameterType.UrlSegment);
            request.AddBody(customer);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ChangeStatusResponse { Id = customer.Id, Code = customer.Code, Message = response.ErrorMessage };
            }
            
            return null;
        }

        #endregion
    }
}