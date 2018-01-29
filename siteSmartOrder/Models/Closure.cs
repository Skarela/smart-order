using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Closure
    {
        public int ClosureId { set; get; }
        public string ClosureName { set;get;}

        public Closure()
        {
        }

        public Closure(int closureId)
        {
            this.ClosureId = closureId;
        }

    }
}
