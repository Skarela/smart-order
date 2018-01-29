using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models.Audit
{
    public class AuditsByCustomer
    {
        public Customer Customer { get; set; }
        public List<Audit> Audits { get; set; }
    }
}