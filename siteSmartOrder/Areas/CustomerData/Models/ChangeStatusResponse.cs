using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.CustomerData.Models
{
    public class ChangeStatusResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}