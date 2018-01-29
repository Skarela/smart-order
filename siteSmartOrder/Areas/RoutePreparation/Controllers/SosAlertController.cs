using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Resolvers;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class SosAlertController : Controller
    {
        private readonly ISosAlertService _alertsService;
        private readonly IUserService _userService;
        private readonly IIncidentService _incidentService;
        private readonly IRouteService _routeService;
        private readonly IJsonFactory _jsonFactory;

        public SosAlertController(ISosAlertService alertsService, IJsonFactory jsonFactory, IUserService userService, IIncidentService incidentService, IRouteService routeService)
        {
            _alertsService = alertsService;
            _jsonFactory = jsonFactory;
            _userService = userService;
            _incidentService = incidentService;
            _routeService = routeService;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public PartialViewResult _NewComment(int userId, int incidentId, int routeId)
        {
            try
            {
                var user = _userService.Get(userId);
                var incident = _incidentService.Get(incidentId);
                var route = _routeService.Get(routeId);
                ViewData["UserName"] = user.Name;
                ViewData["IncidentName"] = incident.Name;
                ViewData["RouteName"] = route.Name;
                ViewData["PhoneNumber"] = route.PhoneNumber;
                return PartialView("_NewComment");
            }
            catch (Exception)
            {
                return PartialView("Error");
            }
        }

        [HttpGet]
        public JsonResult Filter(SosAlertFilter sosAlertFilter)
        {
            try
            {
                var response = _alertsService.Filter(sosAlertFilter);
                return _jsonFactory.Success(response.Alerts, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult GetMultimedias(int alertId, EMultimediaType multimediaType)
        {
            try
            {
                var response = _alertsService.GetMultimedias(alertId, multimediaType);
                return _jsonFactory.Success(response.Multimedias);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(SosAlertFilter sosAlertFilter)
        {
            try
            {
                var responseSosAlerts = _alertsService.Filter(sosAlertFilter);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,INCIDENCIA,RUTA,TÉLEFONO,COMENTARIO,ÚLTIMA ACTUALIZACIÓN,ESTADO");

                excel = (from alert in responseSosAlerts.Alerts
                         let user = _userService.Get(alert.UserId)
                         let route = _routeService.Get(alert.RouteId)
                         let incident = _incidentService.Get(alert.IncidentId)
                         select user.Name + "," + incident.Name + "," + route.Name + "," + route.PhoneNumber + "," + alert.Comment + "," + alert.UpdatedAt + "," + alert.Status.ResolverStatus()).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Alertas " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public JsonResult Starting(SosAlertComment alertsComment)
        {
            try
            {
                _alertsService.Starting(alertsComment);
                 return _jsonFactory.Success("Alerta en progreso.");
            }
            catch (Exception)
            {
                return _jsonFactory.Failure();
            }
        }

        [HttpPost]
        public JsonResult Finalizing(SosAlertComment alertsComment)
        {
            try
            {
                _alertsService.Finalizing(alertsComment);
                return _jsonFactory.Success("Alerta finalizada.");
            }
            catch (Exception)
            {
                return _jsonFactory.Failure();
            }
        }

        #endregion

    }
}
