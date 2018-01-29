using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Pages
{
    public class CustomerPage : Page
    {
        public CustomerPage()
        {
            Customers = new List<Customer>();
        }

        public List<Customer> Customers { get; set; }
    }
}