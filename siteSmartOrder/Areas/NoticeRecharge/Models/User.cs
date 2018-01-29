using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace siteSmartOrder.Areas.NoticeRecharge.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public List<Route> Routes { get; set; }
        public bool MailEnabled { get; set; }
        public bool PhoneNumberEnabled { get; set; }

        public int BranchId { get; set; }

        public List<int> RoutesIds { get; set; }

        public User()
        {
            /*Id = 0;
            Name = "";
            Mail = "";
            PhoneNumber = "";
            Routes = new List<Route>();
            RoutesIds = new List<int>();
            MailEnabled = false;
            PhoneNumberEnabled = false;
            BranchId = 0;*/
        }
    }
}