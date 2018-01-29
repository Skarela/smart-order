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
    public class UserRepository:IUserRepository
    {
        #region IUserRepository Members

        public List<User> GetByBranch(int branchId)
        {
            List<User> users = new List<User>();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/UserNoticeRecharge/GetByBranch/{branchId}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("branchId", branchId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                users = JsonConvert.DeserializeObject<List<User>>(content);
            }
            

            return users;
        }

        public User Get(int Id)
        {
            User user = new User();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/UserNoticeRecharge/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                user = JsonConvert.DeserializeObject<User>(content);
            }
            return user;
        }

        public User Create(User user)
        {
            User entity = new User();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/UserNoticeRecharge", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                string content = response.Content;
                entity = JsonConvert.DeserializeObject<User>(content);
            }
            return entity;
        }

        public User Update(int id, User user)
        {
            User entity = new User();

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/UserNoticeRecharge/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddBody(user);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                entity = JsonConvert.DeserializeObject<User>(content);
            }

            return entity;
        }

        public int AssignRoutes(int id, User user)
        {
            int rowsAffected = 0;

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/UserNoticeRecharge/{id}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddBody(user.RoutesIds);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                rowsAffected = JsonConvert.DeserializeObject<int>(content);
            }
            return rowsAffected;
        }

        public int Deactivate(int id)
        {

            int idDeleted = 0;
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderApi"]);
            var request = new RestRequest("api/UserNoticeRecharge/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = response.Content;
                idDeleted = JsonConvert.DeserializeObject<int>(content);
            }

            return idDeleted;
        }

        #endregion
    }
}