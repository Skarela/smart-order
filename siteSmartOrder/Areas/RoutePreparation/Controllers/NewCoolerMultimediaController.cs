using System;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class NewCoolerMultimediaController : Controller
    {
        private readonly INewCoolerMultimediaService _newCoolerMultimediaService;
        private readonly IJsonFactory _jsonFactory;

        public NewCoolerMultimediaController(INewCoolerMultimediaService newCoolerMultimediaService, IJsonFactory jsonFactory)
        {
            _newCoolerMultimediaService = newCoolerMultimediaService;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [HttpGet]
        public JsonResult Filter(NewCoolerMultimediaFilter newCoolerMultimediaFilter)
        {
            try
            {
                var response = _newCoolerMultimediaService.Filter(newCoolerMultimediaFilter);
                return _jsonFactory.Success(response.NewCoolerMultimedias, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
