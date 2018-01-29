using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models.Audit
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ExceptionType { get; set; }
    }
}