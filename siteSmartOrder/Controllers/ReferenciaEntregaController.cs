using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Models;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;

namespace siteSmartOrder.Controllers
{
    public class ReferenciaEntregaController : Controller
    {
        //
        // GET: /ReferenciaEntrega/

        public ActionResult Index()
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal.branch == null)
            {
                var branches = new List<Branch>();
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("Branch/All", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { code = userPortal.code });
                var response = client.Execute(request);
                string content = response.Content;
                branches = JsonConvert.DeserializeObject<List<Branch>>(content);
                return View(branches);
            }
            return View(new List<Branch> { new Branch { branchId = userPortal.branch.branchId, name = userPortal.branch.name } });
        }

        public JsonResult GetDeliveryReferences()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("GetDeliveryReferences", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            string content = response.Content;
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegisterReference(string description, int value)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("RegisterDeliveryReference", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Description = description, Value = value });
            var response = client.Execute(request);
            string content = response.Content;
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateReference(string description, int value, int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("UpdateDeliveryReference", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { ReferenceId = id, Description = description, Value = value });
            var response = client.Execute(request);
            string content = response.Content;
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteReference(int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("DeleteDeliveryReference", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { DeliveryReferenceId = id });
            var response = client.Execute(request);
            string content = response.Content;
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchDeliveryReferences(string filter)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("SearchDeliveryReferences", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { filter = filter });
            var response = client.Execute(request);
            string content = response.Content;
            return Json(content, JsonRequestBehavior.AllowGet);
        }

    }
}
