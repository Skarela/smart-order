﻿using System;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class CustomerService : ICustomerService
    {
        private IClient _client;

        public Customer Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("customers/{0}", id);
            try
            {
                return _client.Get<Customer>(uri);
            }
            catch (Exception)
            {
                return new Customer(){Code = "0",Id = 0,Name = "NotFoundId:"+id};
            }
        }

        public CustomerPage Filter(CustomerFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            request.BranchId = SessionSettings.ExistsSessionBranch ?  SessionSettings.SessionBranch.SelectedBranch :  request.BranchId;
            var uri = String.Format("customers");
            return _client.Filter<CustomerPage>(uri, request);
        }
    }
}