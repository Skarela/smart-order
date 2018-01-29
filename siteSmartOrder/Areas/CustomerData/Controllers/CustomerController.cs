using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Areas.CustomerData.Interfaces;
using siteSmartOrder.Areas.CustomerData.Repositories;
using siteSmartOrder.Areas.CustomerData.Models;
using Newtonsoft.Json;
using siteSmartOrder.Controllers;

namespace siteSmartOrder.Areas.CustomerData.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;

        public CustomerController()
        {
            _customerRepository = new CustomerRepository();
        }

        [AuthorizeCustom]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int routeId)
        {
            List<Customer> customers = _customerRepository.Get(routeId);

            return Json(customers.OrderBy(c => Convert.ToInt32(c.Code)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Set(List<Customer> customers)
        {
            List<ChangeStatusResponse>   responses = new List<ChangeStatusResponse>();
            foreach (var customer in customers)
            {
                var response = _customerRepository.Set(customer);
                if (response != null)
                    responses.Add(response);
            }
            return Json(responses, JsonRequestBehavior.AllowGet);
        }

    }
}
