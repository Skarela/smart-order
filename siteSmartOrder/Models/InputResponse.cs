using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class InputResponse
    {
        public int Result { set; get; }
        public Response<int> Orders { set; get; }
        public Response<int> Sales { set; get; }
        public Response<int> BinnacleVisits { set; get; }
        public Response<int> DevolutionProducts { set; get; }
        public string Message { set; get; }
    }
}