using System.Collections.Generic;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Contact
    {
        public Contact()
        {
            Id = 0;
            Name = "";
            Email = "";
            Phone = "";
            AlertIds = new List<int>();
            BranchId = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<int> AlertIds { get; set; }
        public int BranchId { get; set; }
    }
}