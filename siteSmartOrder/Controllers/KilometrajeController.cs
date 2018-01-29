using siteSmartOrder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace siteSmartOrder.Controllers
{
    public class KilometrajeController : Controller
    {
        // GET: Kilometraje
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ImportarExcel(HttpPostedFile excel)
        {
            MileageDataTable tabla = new MileageDataTable();
            tabla.Data = new List<Mileage>();
            List<object> Errores = new List<object>();

            try
            {
                if (excel != null && excel.ContentLength > 0)
                {
                    Stream stream = excel.InputStream;
                    IExcelDataReader reader = null;

                    if (excel.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (excel.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    DataSet result = reader.AsDataSet();
                    var tablaExcel = result.Tables[0];

                    if(tablaExcel.Rows.Count > 0)
                    {
                        for(int i = 0; i < tablaExcel.Rows.Count; i++)
                        {
                            if(i > 0)
                            {
                                try
                                {
                                    if (!tablaExcel.Rows[i].IsNull(0) || !tablaExcel.Rows[i].IsNull(3))
                                    {
                                        Mileage kilometraje = new Mileage
                                        {
                                            BranchId = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[0]),
                                            RouteId = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[1]),
                                            Travels = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[2]),
                                            Monday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[3]),
                                            Tuesday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[4]),
                                            Wednesday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[5]),
                                            Thursday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[6]),
                                            Friday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[7]),
                                            Saturday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[8]),
                                            Sunday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[9])
                                        };

                                        tabla.Data.Add(kilometraje);
                                    }
                                }
                                catch (Exception Error)
                                {
                                    Errores.Add(new { Error = Error.Message.ToString(), Elemento = tablaExcel.Rows[i].ItemArray });
                                }
                            }
                            else
                            {
                                object[] encabezado = tablaExcel.Rows[0].ItemArray;
                                try
                                {
                                    Encabezados e1 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[0].ToString());
                                    Encabezados e2 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[1].ToString());
                                    Encabezados e3 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[2].ToString());
                                    Encabezados e4 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[3].ToString());
                                    Encabezados e5 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[4].ToString());
                                    Encabezados e6 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[5].ToString());
                                    Encabezados e7 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[6].ToString());
                                    Encabezados e8 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[7].ToString());
                                    Encabezados e9 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[8].ToString());
                                    Encabezados e10 = (Encabezados)Enum.Parse(typeof(Encabezados), encabezado[9].ToString());
                                }
                                catch (Exception Error)
                                {
                                    Errores.Add(new { Error = Error, elemento = tablaExcel.Rows[0].ItemArray });
                                }
                            }
                        }
                    }
                    if (Errores.Count > 0)
                    {
                        return Json(new { Success = false, Mensaje = "¡Verifica el archivo, no se cuentan con los datos obligatorios!", Tipo = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch(Exception e) { }

            IDictionary<string, int> campos = new Dictionary<string, int> {
                {"BranchId",  0},
                {"RouteId", 0},
                {"Travels", 0},
                {"Monday", 0},
                {"Tuesday", 0},
                {"Wednesday", 0},
                {"Thursday", 0},
                {"Friday", 0},
                {"Saturday", 0},
                {"Sunday", 0}
            };

            //var campoOrdenamiento = campos.First().Value;
            //campos.TryGetValue(campo, out campoOrdenamiento);

            return Json(tabla);
        }

        public enum Encabezados : int
        {
            [Display(Name = "Punto de Venta")]
            BRANCH,
            [Display(Name = "Ruta")]
            ROUTE,
            [Display(Name = "Número de Viajes")]
            TRAVELS,
            [Display(Name = "Lunes")]
            MONDAY,
            [Display(Name = "Martes")]
            TUESDAY,
            [Display(Name = "Miércoles")]
            WEDNESDAY,
            [Display(Name = "Jueves")]
            THURSDAY,
            [Display(Name = "Viernes")]
            FRIDAY,
            [Display(Name = "Sábado")]
            SATURDAY,
            [Display(Name = "Domingo")]
            SUNDAY

        }
    }
}