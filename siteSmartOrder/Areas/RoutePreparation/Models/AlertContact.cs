using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class AlertContact
    {
        public AlertContact()
        {
            AlertId = 0;
            ContactId = 0;
        }

        public int AlertId { get; set; }
        public int ContactId { get; set; }
    }
}