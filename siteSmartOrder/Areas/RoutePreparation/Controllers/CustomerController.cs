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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IJsonFactory _jsonFactory;

        public CustomerController(ICustomerService customerService, IJsonFactory jsonFactory)
        {
            _customerService = customerService;
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
                var customer = _customerService.Get(id);
                return _jsonFactory.Success(customer);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(CustomerFilter customeryFilter)
        {
            try
            {
                var response = _customerService.Filter(customeryFilter);
                return _jsonFactory.Success(response.Customers, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(CustomerFilter customerFilter)
        {
            try
            {
                var responseCustomers = _customerService.Filter(customerFilter);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "CÓDIGO,NOMBRE");
                excel = excel.ConcatRows(0, "Code,Name", responseCustomers.Customers);

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Clientes " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
