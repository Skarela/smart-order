using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using siteSmartOrder.Models;
using System;

namespace siteSmartOrder.Controllers
{
    public class CatalogoController : Controller
    {
        //
        // GET: /Catalogo/
        [AuthorizeCustom]
        public ActionResult Index()
        {

            //var userPortal = (UserPortal)Session["UserPortal"];
            //if (userPortal.branch == null)
            //{
            //    var client = new RestClient();
            //    client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            //    var request = new RestRequest("Branch/All", Method.POST);
            //    request.RequestFormat = DataFormat.Json;
            //    request.AddBody(new { code = userPortal.code });
            //    var response = client.Execute(request);
            //    string content = response.Content;
            //    var branches = JsonConvert.DeserializeObject<List<Branch>>(content);
            //    return View(branches);
            //}
            //return View(new List<Branch> { new Branch { branchId = userPortal.branch.branchId, name = userPortal.branch.name } });
            return View(new List<Branch>());
        }

    }
}
