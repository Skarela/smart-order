using System;
using System.Collections.Generic;
using System.Web.Mvc;
using siteSmartOrder.Infrastructure.Enums;
using siteSmartOrder.Infrastructure.Exceptions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Infrastructure.Factories
{
    public class JsonFactory : Controller, IJsonFactory
    {
        public JsonResult Success()
        {
            return Json(new { Result = Alert.Success.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Success(string message)
        {
            return Json(new { Result = Alert.Success.ToString(), Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Success<T>(T model)
        {
            return Json(new { Result = Alert.Success.ToString(), Record = model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Success<T>(List<T> models)
        {
            return Json(new { Result = Alert.Success.ToString(), Records = models }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Success<T>(List<T> models, int count)
        {
            return Json(new { Result = Alert.Success.ToString(), Records = models, Count = count },
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult Information(string message)
        {
            return Json(new { Result = Alert.Information.ToString(), Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Failure()
        {
            return Json(new { Result = Alert.Failure.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Failure(string message, Type typeException)
        {
            var result = Alert.Failure.ToString();

            if (typeException == new PreconditionFailedException().GetType()) result = Alert.Precondition.ToString();
            if (typeException == new ExpectationFailedException().GetType()) result = Alert.LostSession.ToString();

            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LostSession(string message)
        {
            return Json(new { Result = Alert.LostSession.ToString(), Message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}