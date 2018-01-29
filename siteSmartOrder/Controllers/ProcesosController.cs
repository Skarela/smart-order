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
using System.IO;
using System.Net.Mime;

namespace siteSmartOrder.Controllers
{
    public class ProcesosController : Controller
    {
        string[] allowedExtensions = {".txt"};
        //
        // GET: /Procesos/
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
                branches.Insert(0, new Branch { branchId = 0, code = "name", name = "Todos" });
                return View(branches);
            }
           return View(new List<Branch> { new Branch { branchId = userPortal.branch.branchId, name = userPortal.branch.name } });
        }

        public JsonResult GetProcesses(string branchId, string type)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("processes/{code}/{date}/{type}/branches/{branchId}", Method.GET);
            request.AddParameter("code", userPortal.code, ParameterType.UrlSegment);
            request.AddParameter("date", DateTime.Now.ToString("dd-MM-yyyy"), ParameterType.UrlSegment);
            request.AddParameter("type", type, ParameterType.UrlSegment);
            request.AddParameter("branchId", branchId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            string content = response.Content;

            return Json(new { Data = content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSummary(string processId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("GetSummaries/{processId}/Process/{code}/codeUser", Method.GET);
            request.AddParameter("code", userPortal.code, ParameterType.UrlSegment);
            request.AddParameter("processId", processId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            string content = response.Content;

            return Json(new { Data = content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLogError(string processId)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("GetLogErros/{processId}/Process/{code}/codeUser", Method.GET);
            request.AddParameter("code", userPortal.code, ParameterType.UrlSegment);
            request.AddParameter("processId", processId, ParameterType.UrlSegment);
            var response = client.Execute(request);
            string content = response.Content;

            return Json(new { Data = content }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult GetProcessPercent(string processId)
        {
            var userPortal = (UserPortal) Session["UserPortal"];

            if(userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("Process/Percent", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {Code = userPortal.code, ProcessId = processId});
            var response = client.Execute(request);

            return Content(response.Content);
        }

        public ContentResult CancelProcess(string processId)
        {
            var userPortal = (UserPortal) Session["UserPortal"];

            if(userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("Process/Cancell/Portal", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {Code = userPortal.code, ProcessId = processId});
            var response = client.Execute(request);
            
            string content = (response.StatusCode == System.Net.HttpStatusCode.OK) 
                ? response.Content
                : JsonConvert.SerializeObject(new Response<bool>{ IsSuccess = false, Message = "Ocurrio un error : " + response.StatusDescription });

            return Content(content);
        }

        public JsonResult GetFiles()
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            string filesDirectory = ConfigurationManager.AppSettings["FileLog"];
            var files = Directory.EnumerateFiles(filesDirectory)
                .OrderByDescending(file => new FileInfo(file).CreationTime)
                .Where(file => new FileInfo(file).CreationTime > DateTime.Now.AddDays(-7) 
                    && allowedExtensions.Contains(new FileInfo(file).Extension.ToLower()))
                .Select(file => new
                {
                    Name = Path.GetFileNameWithoutExtension(new FileInfo(file).Name),
                    Extension = new FileInfo(file).Extension,
                    CreatedOn = new FileInfo(file).CreationTime.ToString("dd/MM/yyyy")
                });

            return Json(new { Data = JsonConvert.SerializeObject(new { Data = files }) }, JsonRequestBehavior.AllowGet);
        }

        public FileResult Download(string fileName, string fileExtension)
        {
            var userPortal = (UserPortal)Session["UserPortal"];

            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            if(string.IsNullOrEmpty(fileName))
            {
                Response.StatusCode = 400;
                return null;
            }

            string fullFileName = string.Concat(Server.HtmlEncode(fileName), Server.HtmlEncode(fileExtension));
            string filesDirectory = ConfigurationManager.AppSettings["FileLog"];
            var requestedFile = Directory.EnumerateFiles(filesDirectory)
                .FirstOrDefault(file => new FileInfo(file).Name == string.Concat(fullFileName) 
                    && allowedExtensions.Contains(new FileInfo(file).Extension.ToLower()));

            if (requestedFile != null)
            {
                return File(Path.Combine(filesDirectory, fullFileName), MediaTypeNames.Application.Octet, fullFileName);
            }

            return null;
        }
    }
}
