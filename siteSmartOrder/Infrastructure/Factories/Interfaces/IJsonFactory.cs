using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace siteSmartOrder.Infrastructure.Factories.Interfaces
{
    public interface IJsonFactory
    {
        JsonResult Success();
        JsonResult Success(string message);
        JsonResult Success<T>(T model);
        JsonResult Success<T>(List<T> models);
        JsonResult Success<T>(List<T> models, int count);
        JsonResult Information(string message);
        JsonResult Failure();
        JsonResult Failure(string message, Type typeException);
        JsonResult LostSession(string message);
    }
}