using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Infrastructure.Enums
{
    public enum ExcelHeaders
    {
        [Display(Name = "Punto de Venta")]
        PUNTO_DE_VENTA,
        [Display(Name = "Ruta")]
        RUTA,
        [Display(Name = "Número de Viajes")]
        NUMERO_DE_VIAJES,
        [Display(Name = "Lunes")]
        LUNES,
        [Display(Name = "Martes")]
        MARTES,
        [Display(Name = "Miércoles")]
        MIERCOLES,
        [Display(Name = "Jueves")]
        JUEVES,
        [Display(Name = "Viernes")]
        VIERNES,
        [Display(Name = "Sábado")]
        SABADO,
        [Display(Name = "Domingo")]
        DOMINGO

    }
}