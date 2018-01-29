using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class IncidentController : Controller
    {
        private readonly IIncidentService _incidentsService;
        private readonly IJsonFactory _jsonFactory;

        public IncidentController(IIncidentService incidentsService, IJsonFactory jsonFactory)
        {
            _incidentsService = incidentsService;
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
        public JsonResult Get(int id)
        {
            try
            {
                var incident = _incidentsService.Get(id);
                return _jsonFactory.Success(incident);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(IncidentFilter incidentFilter)
        {
            try
            {
                var response = _incidentsService.Filter(incidentFilter);
                return _jsonFactory.Success(response.Incidents, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(IncidentFilter incidentFilter)
        {
            try
            {
                var responseIncidents = _incidentsService.Filter(incidentFilter);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "NOMBRE");
                excel = excel.ConcatRows(0, "Name", responseIncidents.Incidents);

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Incidencias" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
