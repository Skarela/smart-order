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
    public class ReporteController : Controller
    {
        //
        // GET: /Reporte/
        [AuthorizeCustom]
        public ActionResult Index()
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal.branch == null)
            {
                var response = new Response<List<Report>>();
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("GetReport", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortal.code });
                var r = client.Execute(request);
                string content = r.Content;
                response = JsonConvert.DeserializeObject<Response<List<Report>>>(content);

                return View(response.Data);
            }
            return View(new List<Report>());
        }

        public JsonResult GetReport()
        {
            try
            {
                var userPortal = (UserPortal)Session["UserPortal"];
                if (userPortal != null)
                {
                    var response = new Response<List<Report>>();
                    var client = new RestClient();
                    client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                    var request = new RestRequest("GetReport", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new { userPortalCode = userPortal.code });
                    var r = client.Execute(request);
                    string content = r.Content;
                    response = JsonConvert.DeserializeObject<Response<List<Report>>>(content);

                    return Json(response.Data, JsonRequestBehavior.AllowGet);
                }
                return Json(new List<Report>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<Report>(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Pedidos()
        {
            return View();
        }

        public ActionResult Ventas()
        {
            return View();
        }
    }
}
