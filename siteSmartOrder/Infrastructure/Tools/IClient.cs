using System;
using System.IO;
using System.Web;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;

namespace siteSmartOrder.Infrastructure.Tools
{
    public interface IClient
    {
        TModelResponse Get<TModelResponse>(string uri);
        TModelResponse SpecialMethod<TModelResponse>(IRestRequest restRequest);
        TModelResponse Filter<TModelResponse>(string uri, Object request);
        TModelResponse Filter<TModelResponse>(string uri);
        T Post<T>(string uri, T objectBody);
        T Put<T>(string uri, T objectBody);
        T Put<T>(string uri);
        T Delete<T>(string uri);
        SuccessResponse ChangeStatus<T>(string uri, T objectBody);
        SuccessResponse Assigned<T>(string uri, T objectBody);
        T AddFileByPost<T>(string uri, HttpPostedFileBase file);
        T AddFileByPut<T>(string uri, HttpPostedFileBase file);
        T AddFileStreamByPost<T>(string uri, Stream file, string fileName);
    }
}