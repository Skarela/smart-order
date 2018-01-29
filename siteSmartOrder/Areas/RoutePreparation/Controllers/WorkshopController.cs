using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class WorkshopController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly IWorkshopService _workshopService;
        private readonly IUserPortalService _userPortalService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public WorkshopController(ISurveyService surveyRepository, IWorkshopService workshopService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IUserPortalService userPortalService)
        {
            _surveyRepository = surveyRepository;
            _workshopService = workshopService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _userPortalService = userPortalService;
        }

        #region Get Request

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [AuthorizeCustom]
        public ViewResult New()
        {
            return View("New", new Workshop());
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var workshop = _workshopService.Get(id);
                return View("Edit", workshop);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "Workshop");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var workshop = _workshopService.Get(id);
                return _jsonFactory.Success(workshop);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(WorkshopFilter workshopFilter)
        {
            try
            {
                var response = _workshopService.Filter(workshopFilter);
                return _jsonFactory.Success(response.Workshops, response.TotalRecords);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(WorkshopFilter workshopFilter)
        {
            try
            {
                var responseWorkshops = _workshopService.Filter(workshopFilter);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "ENCUESTA,AUTOR");

                excel = (from workshop in responseWorkshops.Workshops
                         let survey = _surveyRepository.GetFlat(workshop.SurveyId)
                         let userPortal = _userPortalService.Get(workshop.UserPortalId)
                         select survey.Name + "," + userPortal.Name).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
                         );
               
                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Revisión de Taller " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public ActionResult Create(Workshop workshop, Survey survey)
        {
            try
            {
                survey.Category.Id = (int)CategoryType.Workshop;
                _surveyRepository.Create(survey);
                workshop.SurveyId = survey.Id;
                _workshopService.Create(workshop);
                _alertFactory.CreateSuccess(this, "Revisión de Taller creada con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New", "Workshop") : RedirectToAction("index", "Workshop");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("New", workshop);
            }
        }

        [HttpPost]
        public ActionResult Update(Workshop workshop, Survey survey)
        {
            try
            {
                _surveyRepository.Copy(survey);
                workshop.SurveyId = survey.Id;
                _workshopService.Update(workshop);
                _alertFactory.CreateSuccess(this, "Revisión de Taller editada con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New", "Workshop") : RedirectToAction("Index", "Workshop");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Edit", "Workshop", new { id = workshop.Id });
            }
        }

        #endregion

        #region Delete Request

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _workshopService.Delete(id);
                return _jsonFactory.Success("Revisión de Taller eliminada con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
