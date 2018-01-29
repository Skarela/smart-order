using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public ManagerController(IManagerService managerService, IAlertFactory alertFactory, IJsonFactory jsonFactory)
        {
            _managerService = managerService;
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
            return View("New", new Manager());
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var manager = _managerService.Get(id);
                return View("Edit", manager);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "Manager");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var campaign = _managerService.Get(id);
                return _jsonFactory.Success(campaign);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(ManagerFilter managerFilter)
        {
            try
            {
                var response = _managerService.Filter(managerFilter);
                return _jsonFactory.Success(response.Managers, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult GetIncidents(int id)
        {
            try
            {
                var campaign = _managerService.Get(id);
                return _jsonFactory.Success(campaign);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(ManagerFilter managerFilter)
        {
            try
            {
                var responseManagers = _managerService.Filter(managerFilter);
                //responseCampaigns.Campaigns = responseCampaigns.Campaigns.OrderBy(campaignFilter.SortBy);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "NOMBRE,COMPAÑÍA,DIRECCIÓN,EMAIL,TELÉFONO");
                excel = excel.ConcatRows(0, "Name,Company,Address,Email,Phone", responseManagers.Managers);

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Contactos " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public ActionResult Create(Manager manager)
        {
            try
            {
                _managerService.Create(manager);
                _alertFactory.CreateSuccess(this, "Contacto creado con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New") : RedirectToAction("index");
            }
            catch (Exception e)
            {
                if (manager.Id.IsEqualToZero())
                {
                    _alertFactory.CreateFailure(this, e.Message);
                    return RedirectToAction("New"); 
                }
                _alertFactory.CreateFailure(this, "Contacto creado con éxito, con error en la carga de la imagen o asignación de sucursales!");
                return View("Edit", manager);
            }
        }

        [HttpPost]
        public ActionResult Update(Manager manager)
        {
            try
            {
                _managerService.Update(manager);
                _alertFactory.CreateSuccess(this, "Contacto creado con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New") : RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Edit", manager);
            }
        }

        #endregion

        #region Delete Request

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _managerService.Delete(id);
                return _jsonFactory.Success("Contacto eliminado con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
