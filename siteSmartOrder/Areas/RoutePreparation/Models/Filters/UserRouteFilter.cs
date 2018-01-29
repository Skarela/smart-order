using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class UserRouteFilter: RouteFilter
    {
        public string UserName { get; set; } //Nombre del colaborador asociado a la ruta
    }
}