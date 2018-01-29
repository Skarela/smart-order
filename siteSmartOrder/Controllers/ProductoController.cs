using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using System.Configuration;
using System.Web.Script.Serialization;
using siteSmartOrder.Models;
using siteSmartOrder.Util;

namespace siteSmartOrder.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        public JsonResult Paginacion(string page, string branchId, string filter)
        {
            var userPortal = (UserPortal)Session["UserPortal"];
                if (userPortal == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var client = new RestClient();
                string cantPaginacion = ConfigurationManager.AppSettings["Paging"];
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("Product/All?page={page}&num={num}&filter={filter}", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddUrlSegment("page", page);
                request.AddUrlSegment("num", cantPaginacion);
                request.AddUrlSegment("filter", filter);
                int branch = 0;
                if (branchId.Equals("undefined"))
                    branch = userPortal.branch.branchId;
                else
                    branch = int.Parse(branchId);
                request.AddBody(new { code = userPortal.code, branchId = branch });
                var response = client.Execute(request);
                string content = response.Content;
                return Json(content, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult Paginacion_(string page, string branchId, string filter)
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var client = new RestClient();
            string cantPaginacion = ConfigurationManager.AppSettings["Paging"];
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("Product/All?page={page}&num={num}&filter={filter}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("page", page);
            request.AddUrlSegment("num", cantPaginacion);
            request.AddUrlSegment("filter", filter);
            int branch = 0;
            if (branchId.Equals("undefined"))
                branch = userPortal.branch.branchId;
            else
                branch = int.Parse(branchId);
            request.AddBody(new { code = userPortal.code, branchId = branch });
            var response = client.Execute(request);
            var content = response.Content;
            var jss = new JavaScriptSerializer();
            var res = jss.Deserialize<Response<TableObject<List<Product>>>>(content);
            return Json(res.Data.Data, JsonRequestBehavior.AllowGet);
        }

    }
}
