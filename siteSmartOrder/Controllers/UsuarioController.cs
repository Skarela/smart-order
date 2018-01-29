using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using siteSmartOrder.Models;
using RestSharp;
using siteSmartOrder.Controllers;
using siteSmartOrder.Util;

namespace sma
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        [AuthorizeCustom]
        public ActionResult Index()
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal.branch == null)
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("Branch/All", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { code = userPortal.code });
                var response = client.Execute(request);
                string content = response.Content;
                var branches = JsonConvert.DeserializeObject<List<Branch>>(content);
                return View(branches);
            }         
                return View(new List<Branch>{new Branch{branchId = userPortal.branch.branchId,name = userPortal.branch.name}});
        }

        public ContentResult Inactivar(string userId, string deviceId) {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderServer"]);
            var request = new RestRequest("User/Inactivate", Method.POST);
            request.RequestFormat = DataFormat.Json;
            string userCode = userPortal.code;
            request.AddBody(new { Code = userCode, UserId = userId, DeviceId = deviceId });
            var RestResponse = client.Execute(request);
            string content = "";
            if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                content = RestResponse.Content;
            }
            else {
                content = JsonConvert.SerializeObject(new Response<bool>{ IsSuccess = false,Message = "Ocurrio un error : " +RestResponse.StatusDescription});
            }
            return Content(content);
        }

        
        public JsonResult Paginacion(string page,string type, string branchId,string filter)
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var client = new RestClient();
            int pagina = int.Parse(page);
            int cantPaginacion = int.Parse(ConfigurationManager.AppSettings["Paging"]);
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("User/" + type + "?page={page}&num={num}&filter={filter}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("page", page);
            request.AddUrlSegment("num", cantPaginacion.ToString());
            request.AddUrlSegment("filter", filter);
            int branch = 0;
            if (branchId.Equals("undefined"))
                branch = userPortal.branch.branchId;
            else
                branch = int.Parse(branchId);
            string userCode = userPortal.code;
            request.AddBody(new { code = userCode, branchId = branch });
            var response = client.Execute(request);
            string content = response.Content;
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom]
        public ContentResult GeneraInformacion(string branchId) {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderServer"]);
            var request = new RestRequest("GenerateInfo", Method.POST);
            request.RequestFormat = DataFormat.Json;
            int branch = 0;
            if (branchId.Equals("undefined"))
                branch = userPortal.branch.branchId;
            else
                branch = int.Parse(branchId);
            request.AddBody(new { Code = userPortal.code, BranchId = branchId });
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

        [AuthorizeCustom]
        public ContentResult ProcesoPorcent(string processId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("Process/Percent", Method.POST);
            request.RequestFormat = DataFormat.Json;
            int ProcessId = int.Parse(processId);
            request.AddBody(new { Code = userPortal.code, Type= 1, ProcessId = ProcessId });
            var response = client.Execute(request);
            string content = response.Content;
            return Content(content);
        }

    }
}
