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

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class ContactService : IContactService
    {
        private IClient _client;

        public Contact Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("contacts/{0}", id);
            return _client.Get<Contact>(uri);
        }

        public ContactPage Filter(ContactFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("contacts");
            return _client.Filter<ContactPage>(uri, request);
        }

        public void Create(Contact contact)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("contacts");
            var response = _client.Post(uri, contact);
            contact.Id = response.Id;

            if (contact.AlertIds.IsNotNull())
            {
                foreach (var alert in contact.AlertIds.Where(alertId => alertId.IsGreaterThanZero()))
                    AssignAlert(contact.Id, alert);
            }
        }

        public void Update(Contact contact)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("contacts");
            var response = _client.Put(uri, contact);
            contact.Id = response.Id;
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("contacts/{0}", id);
            _client.Delete<SuccessResponse>(uri);
        }

        private void AssignAlert(int contactId, int alertId)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyEngineApi });
            var uri = String.Format("contacts/alert/");
            var request = new AlertContact {AlertId = alertId, ContactId = contactId};
            _client.Assigned(uri, request);
        }
    }
}