using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using RestSharp;
using siteSmartOrder.Models;

namespace siteSmartOrder.Controllers
{
    public class JornadaController : Controller
    {
        //
        // GET: /Jornada/
        [AuthorizeCustom]
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
                request.AddBody(new {code = userPortal.code});
                var response = client.Execute(request);
                string content = response.Content;
                branches = JsonConvert.DeserializeObject<List<Branch>>(content);
                return View(branches);
            }
            return View(new List<Branch> { new Branch { branchId = userPortal.branch.branchId, name = userPortal.branch.name } });
        }

        public JsonResult GetJorneys(string branchId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("branches/{code}/{branchId}/workdays", Method.GET);
            request.AddParameter("code", userPortal.code, ParameterType.UrlSegment);
            request.AddParameter("branchId", branchId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            string content = response.Content;

            return Json(new { Data = content }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult finishJorneys(string workDayId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("ClosingJourneyPortal", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { code =  userPortal.code, workDayId = workDayId });

            var RestResponse = client.Execute(request);
            string content = "";
            if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                content = RestResponse.Content;
            }
            else
            {
                content = JsonConvert.SerializeObject(new Response<bool>{ IsSuccess = false, Message = "Ocurrio un error : " + RestResponse.StatusDescription });
            }
            return Content(content);
        }

        public ContentResult ForceFinishJorney(string workDayId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("ClosingJourneyForceByPortal", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { code = userPortal.code, workDayId = workDayId });

            var RestResponse = client.Execute(request);
            string content = "";
            if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                content = RestResponse.Content;
            }
            else
            {
                content = JsonConvert.SerializeObject(new Response<bool> { IsSuccess = false, Message = "Ocurrio un error : " + RestResponse.StatusDescription });
            }
            return Content(content);
        }

    }
}
