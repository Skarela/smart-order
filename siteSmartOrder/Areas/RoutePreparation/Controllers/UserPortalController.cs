using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class UserPortalController : Controller
    {
        private readonly IUserPortalService _userPortalyService;
        private readonly IJsonFactory _jsonFactory;

        public UserPortalController(IUserPortalService userPortalyService, IJsonFactory jsonFactory)
        {
            _userPortalyService = userPortalyService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var userPortal = _userPortalyService.Get(id);
                return _jsonFactory.Success(userPortal);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(UserPortalFilter userPortalyFilter)
        {
            try
            {
                var response = _userPortalyService.Filter(userPortalyFilter);
                return _jsonFactory.Success(response.UserPortals, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
