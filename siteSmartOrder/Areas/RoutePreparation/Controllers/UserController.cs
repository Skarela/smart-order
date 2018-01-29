using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _useryService;
        private readonly IJsonFactory _jsonFactory;

        public UserController(IUserService useryService, IJsonFactory jsonFactory)
        {
            _useryService = useryService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var user = _useryService.Get(id);
                return _jsonFactory.Success(user);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(UserFilter useryFilter)
        {
            try
            {
                var response = _useryService.Filter(useryFilter);
                return _jsonFactory.Success(response.Users, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
