using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class IncidenceController : Controller
    {

        private readonly IIncidenceService _incidenceService;
        private readonly IRouteService _routeService;
        private readonly IJsonFactory _jsonFactory;

        public IncidenceController( IIncidenceService incidenceService, IJsonFactory jsonFactory, IRouteService routeService)
        {

            _incidenceService = incidenceService;
            _jsonFactory = jsonFactory;
            _routeService = routeService;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public JsonResult Filter(IncidenceFilter incidenceFilter)
        {
            try
            {
                var response = _incidenceService.Filter(incidenceFilter);
                return _jsonFactory.Success(response.Incidences, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(IncidenceFilter incidenceFilter)
        {
            try
            {
                var responseIncidences = _incidenceService.Filter(incidenceFilter);
                var excel = string.Empty;
                excel = excel.ConcatRow(0, "USUARIO,RUTA,CEDIS,CODIGO DE UNIDAD,TIPO,ACCIÓN,FECHA DE CAPTURA");
                excel = (from incidence in responseIncidences.Incidences
                         let route = incidence.RouteId.IsNotNull() ?_routeService.Get(Convert.ToInt32(incidence.RouteId)).Name : "-"
                         select incidence.User + "," + route + "," + incidence.Branch + "," + incidence.UnitCode + "," + incidence.Type + "," + incidence.Action + "," + incidence.CreatedOn).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Incidencias " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
