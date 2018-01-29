using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Areas.NoticeRecharge.Repositories;
using siteSmartOrder.Areas.NoticeRecharge.Models;
using siteSmartOrder.Areas.NoticeRecharge.Interfaces;

namespace siteSmartOrder.Areas.NoticeRecharge.Controllers
{
    public class BranchController : Controller
    {
        //
        // GET: /NoticeRecharge/Branch/
        private IBranchRepository _branchRepository;

        public BranchController()
        {
            _branchRepository = new BranchRepository();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Get()
        {
            List<Branch> branches= new List<Branch>();
            var userPortal = (siteSmartOrder.Models.UserPortal)Session["UserPortal"];
            if (userPortal.branch == null)
                branches = _branchRepository.Get();
            else
                branches = new List<Branch> { new Branch { BranchId = userPortal.branch.branchId, Code = userPortal.branch.code, Name = userPortal.branch.name } };
            
            return Json(branches, JsonRequestBehavior.AllowGet);
        }

    }
}
