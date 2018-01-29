using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Resolvers;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class AlertController : Controller
    {
        private readonly IAlertService _alertService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public AlertController(IAlertService alertService, IAlertFactory alertFactory, IJsonFactory jsonFactory)
        {
            _alertService = alertService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult New()
        {
            return View("New", new Alert());
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var alert = _alertService.Get(id);
                return View("Edit", alert);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "Alert");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var alert = _alertService.Get(id);
                return _jsonFactory.Success(alert);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(AlertFilter alertFilter)
        {
            try
            {
                var response = _alertService.Filter(alertFilter);
                return _jsonFactory.Success(response.Alerts, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult GetTypes()
        {
            try
            {
                var types = new AlertType().ConvertToCollection();
                types = types.Select(type=> new Enumerator{Value = type.Value, Name = type.Value.ResolverAlertType()}).ToList();
                return _jsonFactory.Success(types);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(AlertFilter alertFilter)
        {
            try
            {
                var responseAlerts = _alertService.Filter(alertFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "NOMBRE,DESCRIPCIÓN,TIPO");
                excel = excel.ConcatRows(0, "Name,Description,DisplayType", responseAlerts.Alerts);

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
        public ActionResult Create(Alert alert)
        {
            try
            {
                _alertService.Create(alert);
                _alertFactory.CreateSuccess(this, "Alerta creada con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New", "Alert") : RedirectToAction("index", "Alert");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("New", "Alert");
            }
        }

        [HttpPost]
        public ActionResult Update(Alert alert)
        {
            try
            {
                _alertService.Update(alert);
                _alertFactory.CreateSuccess(this, "Alerta actualizada con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New", "Alert") : RedirectToAction("Index", "Alert");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Edit", "Alert", new { id = alert.Id });
            }
        }

        #endregion

        #region Delete Request

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _alertService.Delete(id);
                return _jsonFactory.Success("Alera eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
