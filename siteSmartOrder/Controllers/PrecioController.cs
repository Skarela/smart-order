using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using siteSmartOrder.Models;
using System.Web.Script.Serialization;
using siteSmartOrder.Util;

namespace siteSmartOrder.Controllers
{
    public class PrecioController : Controller
    {
        //
        // GET: /Precio/

        public JsonResult Paginacion(string page, string branchId, string filter) {
            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var client = new RestClient();
            string cantPaginacion = ConfigurationManager.AppSettings["Paging"];
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("PriceList/All?page={page}&num={num}&filter={filter}", Method.POST);
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
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //var priceLists = js.Deserialize<TableObject<List<PriceList>>>(content);
            //int numeroPaginas = ValidaNumeroPaginas(priceLists.DataCount, int.Parse(cantPaginacion));
            //return Json(new { Data = RenderHelper.PartialView(this, "GridPrecios", priceLists.Data), DataCount = numeroPaginas }, JsonRequestBehavior.AllowGet);
   
        }

        [AuthorizeCustom]
        public ActionResult Detalles(string id) {

            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("PriceList/Detail?page={page}&num={num}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var userPortal = (UserPortal)Session["UserPortal"];
            request.AddUrlSegment("page", "1");
            request.AddUrlSegment("num", "10");
            int pricelistId = int.Parse(id);
          /*  if (branchId.Equals("undefined"))
                branch = userPortal.branchId.Value;
            else
                branch = int.Parse(branchId);*/
            request.AddBody(new { code = userPortal.code, priceListId = pricelistId });
            var response = client.Execute(request);
            string content = response.Content;
            var details = JsonConvert.DeserializeObject<List<PriceListDetail>>(content);

            return View(details);
        
        }
    }
}
