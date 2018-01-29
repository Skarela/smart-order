using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Infrastructure.Exceptions;
using System.Text;

namespace siteSmartOrder.Infrastructure.Tools
{
    public static class ClientValidate
    {
        private static readonly IDictionary<Enum, Exception> Exceptions = new Dictionary<Enum, Exception>
        {            
            { ErrorType.None, new InternalServerException()},
            { ErrorType.InvalidDate, new InvalidDateException()},
            { ErrorType.NotFound, new NotFoundException()},
            { ErrorType.Conflict, new ServerNotFoundException()}
        };

        public static void ThrowIfNotSuccess(IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var exceptionResponse = new JavaScriptSerializer().Deserialize<ExceptionResponse>(response.Content);
                var errorTypeCurrent = (ErrorType)Enum.ToObject(typeof(ErrorType), exceptionResponse.ErrorCode);
                
                if (errorTypeCurrent == ErrorType.InvalidDate) {
                    throw new InvalidDateException(exceptionResponse.Message);
                }else{
                    throw Exceptions[errorTypeCurrent];
                }
            }
        }
    }
}