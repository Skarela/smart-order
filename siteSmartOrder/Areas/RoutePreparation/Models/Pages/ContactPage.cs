using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class ContactPage : Page
    {
        public ContactPage()
        {
            Contacts = new List<Contact>();
        }

        public List<Contact> Contacts { get; set; }
    }
}