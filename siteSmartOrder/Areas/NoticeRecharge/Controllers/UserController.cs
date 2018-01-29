using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siteSmartOrder.Areas.NoticeRecharge.Models;
using siteSmartOrder.Areas.NoticeRecharge.Interfaces;
using siteSmartOrder.Areas.NoticeRecharge.Repositories;
using siteSmartOrder.Controllers;

namespace siteSmartOrder.Areas.NoticeRecharge.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IRouteRepository _routeRepository;
        private IBranchRepository _branchRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
            _routeRepository = new RouteRepository();
            _branchRepository = new BranchRepository();

        }
        
        [AuthorizeCustom]
        public ActionResult Index(int? branchId)
        {
            ViewBag.BranchId = branchId.HasValue ? branchId.Value : 0;
            return View();
        }

        [AuthorizeCustom]
        public ActionResult New(int branchId)
        {
            User user = new User();
            user.BranchId = branchId;
            user.Routes = _routeRepository.GetByBranch(branchId);
            user.RoutesIds = user.Routes.Select(r => r.Id).ToList();

            ViewBag._BranchId = branchId;
            return View(user);
        }

        [AuthorizeCustom]
        public ActionResult Edit(int branchId, int userId)
        {
            User user = _userRepository.Get(userId);
            user.Routes = _routeRepository.GetByBranch(branchId);
            user.RoutesIds = user.Routes.Select(r => r.Id).ToList();

            ViewBag._BranchId = branchId;

            return View(user);
        }


        [HttpGet]
        public JsonResult GetUsers(int branchId)
        {
            List<User> users = _userRepository.GetByBranch(branchId);
            foreach (User user in users)
            {
                user.Routes = _routeRepository.GetByUser(user.Id);
                user.RoutesIds = user.Routes.Select(r => r.Id).ToList();
            }

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        public JsonResult GetRoutes(int userId)
        {
            List<Route> routes = _routeRepository.GetByUser(userId);
            return Json(routes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            User entity = _userRepository.Create(user);
            if (entity.Id != 0)
            {
                _userRepository.AssignRoutes(entity.Id, user);
            }

            return RedirectToAction("Index", "User", new { branchId = user.BranchId });
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            User entity = _userRepository.Update(user.Id, user);
            if (entity.Id != 0)
            {
                _userRepository.AssignRoutes(entity.Id, user);
            }

            return RedirectToAction("Index", "User", new { branchId = user.BranchId });
        }

        public ActionResult Deactivate(int branchId, int userId)
        {
            int rows = _userRepository.Deactivate(userId);
            return RedirectToAction("Index", "User", new { branchId = branchId });
        }
    }
}
