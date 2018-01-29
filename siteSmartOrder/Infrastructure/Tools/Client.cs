using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Infrastructure.Extensions;

namespace siteSmartOrder.Infrastructure.Tools
{
    public class Client: IClient
    {
        private static RestClient _restClient;

        public Client(RestClient restClient)
        {
            _restClient = restClient;
        }

        public  TModelResponse Get<TModelResponse>(string uri)
        {
            var restRequest = new RestRequest(uri, Method.GET);
            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public TModelResponse SpecialMethod<TModelResponse>(IRestRequest restRequest)
        {
            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public TModelResponse Filter<TModelResponse>(string uri, Object request)
        {
            var restRequest = new RestRequest(uri, Method.GET);
            var dictionary = request.ConvertToDictionary();

            if (dictionary.IsNull())
                return ExcecuteRestClient<TModelResponse>(restRequest);

            foreach (var objectParameter in dictionary.Where(parameter => parameter.Value.IsNotNullOrEmpty() &&  parameter.Value != "null"))
                restRequest.AddParameter(objectParameter.Key, objectParameter.Value);

            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public TModelResponse Filter<TModelResponse>(string uri)
        {
            var restRequest = new RestRequest(uri, Method.GET);
            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public T Post<T>(string uri, T objectBody)
        {
            var restRequest = new RestRequest(uri, Method.POST);

            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<T>(restRequest);
        }

        public T Put<T>(string uri, T objectBody)
        {
            var restRequest = new RestRequest(uri, Method.PUT);

            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<T>(restRequest);
        }

        public T Put<T>(string uri)
        {
            var restRequest = new RestRequest(uri, Method.PUT);
            return ExcecuteRestClient<T>(restRequest);
        }

        public T Delete<T>(string uri)
        {
            var restRequest = new RestRequest(uri, Method.DELETE);
            return ExcecuteRestClient<T>(restRequest);
        }

        public SuccessResponse ChangeStatus<T>(string uri, T objectBody)
        {
            var restRequest = new RestRequest(uri, Method.PUT);

            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public SuccessResponse Assigned<T>(string uri, T objectBody)
        {
            var restRequest = new RestRequest(uri, Method.POST);

            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public T AddFileByPost<T>(string uri, HttpPostedFileBase file)
        {
            return AddFile<T>(uri, file, Method.POST);
        }

        public T AddFileByPut<T>(string uri, HttpPostedFileBase file)
        {
            return AddFile<T>(uri, file, Method.PUT);
        }

        private T AddFile<T>(string uri, HttpPostedFileBase file, Method method)
        {
            var restRequest = new RestRequest(uri, method);

            var stream = new MemoryStream();
            file.InputStream.CopyTo(stream);
            stream.Position = 0L;

            if (file.IsNotNull())
                restRequest.AddFile("file", stream.ToArray(), file.FileName, "multipart/form-data");

            return ExcecuteRestClient<T>(restRequest);
        }

        public T AddFileStreamByPost<T>(string uri, Stream file, string fileName)
        {
            return AddFileStream<T>(uri, file, fileName, Method.POST);
        }

        private T AddFileStream<T>(string uri, Stream file, string fileName, Method method)
        {
            var restRequest = new RestRequest(uri, method);

            var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0L;

            if (file.IsNotNull())
                restRequest.AddFile("file", stream.ToArray(), fileName, "multipart/form-data");

            return ExcecuteRestClient<T>(restRequest);
        } 
        private static TModelResponse ExcecuteRestClient<TModelResponse>(IRestRequest restRequest)
        {
            var jss = new JavaScriptSerializer();
            var response = _restClient.Execute(restRequest);
            var responseContent = response.Content;

            ClientValidate.ThrowIfNotSuccess(response);

            var model = jss.Deserialize<TModelResponse>(responseContent);
            return model;
        }
    }
}