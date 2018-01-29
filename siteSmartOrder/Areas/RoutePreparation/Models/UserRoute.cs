using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class UserRoute: Route
    {
        public string UserName { get; set; } //Nombre del colaborador, no del usuario
    }
}