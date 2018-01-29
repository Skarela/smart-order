using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Controllers;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IAlertService _alertService;
        private readonly IBranchService _branchService;
        private readonly IAlertFactory _alertFactory;
        private readonly IJsonFactory _jsonFactory;

        public ContactController(IContactService contactService, IAlertFactory alertFactory, IJsonFactory jsonFactory, IAlertService alertService, IBranchService branchService)
        {
            _contactService = contactService;
            _alertFactory = alertFactory;
            _jsonFactory = jsonFactory;
            _alertService = alertService;
            _branchService = branchService;
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
            return View("New", new Contact());
        }

        [HttpGet]
        [AuthorizeCustom]
        public ActionResult Edit(int id)
        {
            try
            {
                var contact = _contactService.Get(id);
                return View("Edit", contact);
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Index", "Contact");
            }
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                var contact = _contactService.Get(id);
                return _jsonFactory.Success(contact);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public JsonResult Filter(ContactFilter contactFilter)
        {
            try
            {
                var contacts = new List<Contact>();
                var contactwithoutBranchResponse = _contactService.Filter(new ContactFilter { WithoutBranch = true, Name = contactFilter.Name });
                var contactResponse = _contactService.Filter(contactFilter);
                contacts.AddRange(contactwithoutBranchResponse.Contacts);
                contacts.AddRange(contactResponse.Contacts);
                return _jsonFactory.Success(contacts, contacts.Count);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        [HttpGet]
        public FileResult Export(ContactFilter contactFilter)
        {
            try
            {
                var contacts = new List<Contact>();
                var contactwithoutBranchResponse = _contactService.Filter(new ContactFilter { WithoutBranch = true, Name =contactFilter.Name });
                var contactResponse = _contactService.Filter(contactFilter);
                contacts.AddRange(contactwithoutBranchResponse.Contacts);
                contacts.AddRange(contactResponse.Contacts);

                var excel = string.Empty;
                excel = excel.ConcatRow(0, "NOMBRE,EMAIL,TELÉFONO,SUCURSAL");

                excel = (from contact in contacts
                         let branchName = contact.BranchId.IsGreaterThanZero() ? _branchService.Get(contact.BranchId).Name : ""
                         select contact.Name + "," + contact.Email + "," + contact.Phone + "," + branchName).Aggregate(excel, (current, row) => current.ConcatRow(0, row)
         );

                var bytes = Encoding.Unicode.GetBytes(excel);
                var stream = new MemoryStream(bytes);
                return File(stream, "application/csv", "Contactos " + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".csv");
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Post Request

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            try
            {
                _contactService.Create(contact);
                _alertFactory.CreateSuccess(this, "Contacto creado con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New", "Contact") : RedirectToAction("index", "Contact");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("New", "Contact");
            }
        }

        [HttpPost]
        public ActionResult Update(Contact contact)
        {
            try
            {
                _contactService.Update(contact);
                _alertFactory.CreateSuccess(this, "Contacto actualizado con éxito!");
                return Request.Form["View"].Contains("New") ? RedirectToAction("New", "Contact") : RedirectToAction("Index", "Contact");
            }
            catch (Exception e)
            {
                _alertFactory.CreateFailure(this, e.Message);
                return RedirectToAction("Edit", "Contact", new { id = contact.Id });
            }
        }

        #endregion

        #region Delete Request

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _contactService.Delete(id);
                return _jsonFactory.Success("Contacto eliminado con éxito!");
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
