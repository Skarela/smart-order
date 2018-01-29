using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class CoolerController : Controller
    {
        private readonly ICoolerService _cooleryService;
        private readonly IJsonFactory _jsonFactory;

        public CoolerController(ICoolerService cooleryService, IJsonFactory jsonFactory)
        {
            _cooleryService = cooleryService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult Index(int customerId)
        {
            return View("Index", customerId);
        }

        [HttpGet]
        public JsonResult Get(string id)
        {
            try
            {
                var cooler = _cooleryService.Get(id);
                return _jsonFactory.Success(cooler);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(CoolerFilter cooleryFilter)
        {
            try
            {
                var response = _cooleryService.Filter(cooleryFilter);
                return _jsonFactory.Success(response.Coolers, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(CoolerFilter cooleryFilter)
        {
            try
            {
                var responseCoolers = _cooleryService.Filter(cooleryFilter);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "SERIE,NOMBRE,PUERTAS");
                excel = excel.ConcatRows(0, "Serie,Name,DoorsNumber", responseCoolers.Coolers);

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Enfriadores por cliente" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
