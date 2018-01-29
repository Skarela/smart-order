using System;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using siteSmartOrder.Models;
using RestSharp;
using System.Web.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using siteSmartOrder.Models.ViewModel;
using ExcelDataReader;
using System.ComponentModel.DataAnnotations;
using System.Data;
using siteSmartOrder.Infrastructure.Enums;
using siteSmartOrder.Infrastructure.Exceptions.TheoreticalMileage;
using System.Linq;
using System.Text.RegularExpressions;
using siteSmartOrder.Infrastructure.Tools;
using Newtonsoft.Json.Linq;

namespace siteSmartOrder.Controllers
{
    public class ConfiguracionController : Controller
    {
        //
        // GET: /Configuracion/
        [AuthorizeCustom]
        public ActionResult Index(string tab)
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            List<Branch> branches = new List<Branch>();
           
            if(tab== null)
                tab = "Cuenta";
            TempData["tabName"] = tab;

            if (tab.Equals("Importar"))
            {
                if (userPortal.branch == null)
                {
                    var client = new RestClient();
                    client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                    var request = new RestRequest("Branch/All", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new { code = userPortal.code });
                    var response = client.Execute(request);
                    string content = response.Content;
                    branches = JsonConvert.DeserializeObject<List<Branch>>(content);
                    branches.Insert(0, new Branch { branchId = 0, code = "Company", name = "Todas" });
                    return View(branches);
                }
                branches.Add(new Branch { code = userPortal.branch.code, name = userPortal.branch.name, branchId = userPortal.branch.branchId });
                return View(branches);
            }
            return View();
        }
        [AuthorizeCustom]
        public ActionResult Cuenta() 
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            TempData["NickName"] = userPortal.nickname;
            return View();
        }
        [AuthorizeCustom]
        public ActionResult Correo() 
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("SmtpSettings/Get", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Code = userPortal.code });
            var response = client.Execute(request);
            string content = response.Content;
            SmtpSetting setting = JsonConvert.DeserializeObject<SmtpSetting>(content);
            return View(setting);
        }
        [AuthorizeCustom]
        public ActionResult Servicios()
        {
            return View();
        }

        [AuthorizeCustom]
        public ActionResult Cierre()
        {
            var userPortal = (UserPortal)Session["UserPortal"];
            List<Branch> branches = new List<Branch>();
            Closure closureSelected = null;
            if (userPortal.branch == null)
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("Branch/All", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { code = userPortal.code });
                var response = client.Execute(request);
                string content = response.Content;
                branches = JsonConvert.DeserializeObject<List<Branch>>(content);
                if (branches.Capacity == 0)
                {
                    closureSelected = new Closure();
                }
                else
                {
                    closureSelected = new Closure(branches[0].ClosureTypeId);
                }
            }
            else
            {
                closureSelected = new Closure(userPortal.branch.ClosureTypeId);
                branches.Add(userPortal.branch);
            }

            return View(new ClosureViewModel(branches, getDefaultClosures(), closureSelected));
        }

        [AuthorizeCustom]
        public ActionResult Importar(List<Branch> branches)
        {
           /* var userPortal = (UserPortal)Session["CobUser"];
            var client = new RestClient();
            client.BaseUrl = ConfigurationManager.ConnectionStrings["MobileServer"].ConnectionString;
            var request = new RestRequest("Process/Get", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Code = userPortal.code, Type = 2, BranchId = 1 });
            var response = client.Execute(request);
            string content = response.Content;
            JavaScriptSerializer js = new JavaScriptSerializer();
            Response<int> response2 = js.Deserialize<Response<int>>(content);
            if (!response2.IsSuccess) {
                TempData["Process"] = Convert.ToString(response2.Data);
            }*/
            
            return View(branches);
        }

         [AuthorizeCustom]
        public ActionResult Descargar()
         {
             return View();
         }

         [AuthorizeCustom]
         public ActionResult Cargar()
         {
             return View();
         }

        [AuthorizeCustom]
        public ActionResult Licencia()
        {
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderServer"]);
                var request = new RestRequest("License/GetInfo", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { Code = userPortal.code });
                var responseRest = client.Execute(request);
                var response = new Response<string>();
                if (responseRest.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = responseRest.Content;
                    response = JsonConvert.DeserializeObject<Response<string>>(content);
                }
                else {
                    response.IsSuccess = false;
                    response.Message = "Existe un error: "+ responseRest.StatusDescription;
                }
                return View(response);
            
           
        }

        public ContentResult RegistrarLicencia(string key) {

            var userPortal = (UserPortal)Session["UserPortal"];
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderServer"]);
            var request = new RestRequest("License/Set", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Code = userPortal.code, Key= key });
            var RestResponse = client.Execute(request);
            string content = "";
                if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    content = RestResponse.Content;
                }
                else {
                    content = JsonConvert.SerializeObject(new Response<bool>{ IsSuccess = false, Message = "Ocurrio un error : " + RestResponse.StatusDescription });
                }

            return Content(content);
        } 
        

        [AuthorizeCustom]
        [HttpPost]
        public ActionResult AltaInfo()
        {                       
            try
            {
                HttpPostedFileBase file = Request.Files["file"];
                string branchCode = Request.Form["ComboBranchId"];
                string[] splitfilename = file.FileName.Split('.');
                string extension = splitfilename[1];
                if (/*extension.Equals("xlsx")||*/ extension.Equals("xls"))
                {
                       // string fechaActual = DateTime.Now.ToString("ddMMyyyyhhmmss");
                        string fname = Path.GetFileName(file.FileName);
                        string urlFile = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
                        //string fileName = branchCode+"_"+fechaActual + "_CargaBase" + "." + extension;
                        string fileName = "ArchivoDeCarga" + "." + extension;
                        file.SaveAs(urlFile + fileName );
                        TempData.Add("tipo", "success");
                        TempData.Add("result", "Actualizacion Exitosa.");
                        TempData.Add("fileName", fileName);
                }
                else
                {
                    TempData.Add("tipo", "warring");
                    TempData.Add("result", "Archivo de formato no v&aacute;lido. tiene que ser XLS");
                    TempData.Add("fileName", "");
                }

            }
            catch (Exception e)
            {
                TempData.Add("tipo", "error");
                TempData.Add("result", "Error procesando los datos");
                TempData.Add("fileName", "");

            }
            return RedirectToAction("Index");
        }

        [AuthorizeCustom]
        public ContentResult GuardaUrl(string url) {
            try
            {
                Dictionary<string, string> UrlObject = new Dictionary<string, string>();
                UrlObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(url);

                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings["SmartOrderServer"].Value = UrlObject["urlSmartOrder"];
                config.AppSettings.Settings["CreditServer"].Value = UrlObject["urlCredit"]; 
               // AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
               // appSettings.Settings.Remove("MobileServer");
                //appSettings.Settings.Add("MobileServer", url);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
               /* string path = Server.MapPath("~/web.config");
                System.Configuration.Configuration conn = WebConfigurationManager.OpenWebConfiguration("~");
                ConnectionStringSettingsCollection settings = conn.ConnectionStrings.ConnectionStrings;
                conn.ConnectionStrings.ConnectionStrings["MobileServer"].ConnectionString = url;
               // conn.ConnectionStrings.SectionInformation.RestartOnExternalChanges = false;
                conn.Save(ConfigurationSaveMode.Full);*/
            }
            catch (Exception e) { return Content("error..exception: " + e.Message); }
            
            return Content("complete");
        }

        [AuthorizeCustom]
        public JsonResult ChangePassword(string password) {

            try {
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("Account/ChangePassword", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { Code = userPortal.code, Password=password });
                var response = client.Execute(request);
                string content = response.Content;
                return Json(new { Data = content }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                var response = new Response<bool>
                                   {
                                       IsSuccess = false,
                                       Message = "Ocurrio un error inesperado", 
                                       ErrorCode = 500};
                return Json(new { Data = JsonConvert.SerializeObject(response) }, JsonRequestBehavior.AllowGet);
               
            }
        }

        [AuthorizeCustom]
        public JsonResult ChangeServerSmtpSettings(string setting) {

            try
            {
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("SmtpSettings/Set", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { Code = userPortal.code, Setting = setting });
                var response = client.Execute(request);
                string content = response.Content;
                return Json(new { Data = content }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var response = new Response<bool>
                                   {
                                       IsSuccess = false, 
                                       Message = "Ocurrio un error inesperado", 
                                       ErrorCode = 500
                                   };
                return Json(new { Data = JsonConvert.SerializeObject(response) }, JsonRequestBehavior.AllowGet);
            }
         }

        public ContentResult StartEtlProcess(string etlType, string complements) {

            var userPortal = (UserPortal)Session["UserPortal"];
            if (userPortal == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            var client = new RestClient
                             {
                                 BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"])
                             };
            if(etlType.Equals("0"))
            {
                complements = userPortal.userPortalId.ToString();
            }
            var request = new RestRequest("EtlProcess/Start", Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(new { Code = userPortal.code, ETLType = int.Parse(etlType),Complements = complements});
            var responseRest = client.Execute(request);
            string content = responseRest.Content;

            return Content(content);
        }


        [AuthorizeCustom]
        public ContentResult ProcesoPorcent(string processId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("Process/Percent", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var userPortal = (UserPortal)Session["UserPortal"];
            int ProcessId = int.Parse(processId);
            request.AddBody(new { Code = userPortal.code, Type = 2, ProcessId = ProcessId });
            var response = client.Execute(request);
            string content = response.Content;
            return Content(content);
        }

        private void CancelarProceso(int processId, string Code) {
        
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderServer"]);
            var request = new RestRequest("Process/Cancell", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Code = Code, ProcessId = processId });
            var responseRest = client.Execute(request);
            string content = responseRest.Content;
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(content);
        }

        [AuthorizeCustom]
        [HttpPost]
        public ActionResult CargarJson()
        {
            try
            {
                HttpPostedFileBase file = Request.Files["file"];

                string[] splitfilename = file.FileName.Split('.');
                string extension = splitfilename[splitfilename.Length - 1];
                if (extension.Equals("js") || extension.Equals("txt"))
                {
                    string fechaActual = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    string fname = Path.GetFileName(file.FileName);
                    string resultString = new StreamReader(file.InputStream).ReadToEnd();
                    var backupFile = JsonConvert.DeserializeObject<BackupFile>(resultString);
                    var inputData = JsonConvert.DeserializeObject<FileInputPackage>(backupFile.Data);
                    var client = new RestClient();
                    client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SmartOrderServer"]);
                    var request = new RestRequest("SetData", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddHeader("OSToken",backupFile.Token);
                    request.AddBody(new { Data=inputData.Data });
                    var restResponse = client.Execute(request);

                    var response =  JsonConvert.DeserializeObject<Response<string>>(restResponse.Content);
                    if (response.IsSuccess)
                    {
                        
                        if (response.Data == null)
                        {
                            TempData.Add("result", response.Message);
                            TempData.Add("tipo", "warning");
                        }
                        else
                        {
                            var inputResponse = JsonConvert.DeserializeObject<InputResponse>(response.Data);
                            TempData.Add("result", inputResponse.Message);
                            TempData.Add("tipo", "success");
                        }
                    }else
                    {
                        TempData.Add("result", response.Message);
                        TempData.Add("tipo", "error");
                    }

                }
                else
                {
                    TempData.Add("tipo", "warring");
                    TempData.Add("result", "Archivo de formato no v&aacute;lido");
                }

            }
            catch (Exception e)
            {
                TempData.Add("tipo", "error");
                TempData.Add("result", "Error procesando los datos");
            }
            return RedirectToAction("Index",new{tab="Cargar"});
        }

        /// <summary>
        /// Kilometraje Teórico: Cargar excel, validar condiciones y desplegar Data Table
        /// </summary>
        /// <returns>Mensaje de errores y tabla con registros erroneos (tabla completa
        /// si no existen errores)</returns>
        /// 

        [AuthorizeCustom]
        public ActionResult Kilometraje()
        {
            return View();
        }

        public JsonResult CargarKilometraje()
        {
            HttpPostedFileBase excel = Request.Files[0];
            MileageDataTable tabla = new MileageDataTable();
            tabla.Data = new List<Mileage>();
            tabla.DataWithErrors = new List<MileageError>();

            try
            {
                string[] splitfilename = excel.FileName.Split('.');
                string extension = splitfilename[1];

                if (excel != null && excel.ContentLength > 0)
                {
                    Stream stream = excel.InputStream;
                    IExcelDataReader reader = null;

                    if (excel.FileName.EndsWith(".xls"))  reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else if (excel.FileName.EndsWith(".xlsx")) reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    else return Json(new { Success = false, Mensaje = "¡Archivo no válido, por favor seleccione un archivo .xls o .xlsx (Excel)!", Tipo = 0 }, JsonRequestBehavior.AllowGet);

                    DataSet result = reader.AsDataSet();
                    var tablaExcel = result.Tables[0];

                    if (tablaExcel.Rows.Count > 0)
                    {
                        var rowList = tablaExcel.AsEnumerable().Select(x => x.ItemArray).ToList();
                        //var sinDatos = rowList.Where(x => x.Any(y => y.value == null || y.value == DBNull.Value)).ToList();
                        var sinDatos = rowList.Select((value, index) => new { value, index })
                            .Where(x => x.value.Any(y => y == null || y == DBNull.Value))
                            .Select(x => x.index).ToList();
                        
                        if (!sinDatos.Any())
                        {
                            for (int i = 0; i < tablaExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {
                                    try
                                    {
                                        if (!Regex.IsMatch(Convert.ToString(tablaExcel.Rows[i].ItemArray[0]), MileageRegex.BranchLength))
                                        {
                                            tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Punto de Venta no debe contener más de 5 dígitos." });
                                        }

                                        if (!Regex.IsMatch(Convert.ToString(tablaExcel.Rows[i].ItemArray[1]), MileageRegex.RouteLength))
                                        {
                                            tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Punto de Venta no debe contener más de 4 dígitos." });
                                        }

                                        if (!Regex.IsMatch(Convert.ToString(tablaExcel.Rows[i].ItemArray[2]), MileageRegex.TravelsLength))
                                        {
                                            tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Punto de Venta no debe contener más de 2 dígitos." });
                                        }

                                        for (int j = 3; j < tablaExcel.Columns.Count; j++)
                                        {
                                            if (!Regex.IsMatch(Convert.ToString(tablaExcel.Rows[i].ItemArray[j]), MileageRegex.DayOfWeekLength))
                                            {
                                                tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Punto de Venta no debe contener más de 3 dígitos." });
                                            }
                                        }

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
                                            Sunday = Convert.ToInt32(tablaExcel.Rows[i].ItemArray[9]),
                                            Row = i
                                        };

                                        var repetido = tabla.Data.AsEnumerable().FirstOrDefault(x =>
                                                       x.BranchId == kilometraje.BranchId &&
                                                       x.RouteId == kilometraje.RouteId &&
                                                       x.Travels == kilometraje.Travels);

                                        if(repetido != null)
                                        {
                                            tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Los identificadores son idénticos a los de la fila: " + repetido.Row });
                                        }

                                        else
                                        {
                                            tabla.Data.Add(kilometraje);
                                        }
                                     
                                    }

                                    catch (Exception e)
                                    {
                                        tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Todos los datos deben ser valores numéricos." });
                                    }
                                }

                                //else
                                //{
                                //    object[] encabezado = tablaExcel.Rows[0].ItemArray;
                                //    try
                                //    {
                                //        ExcelHeaders e1 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[0].ToString());
                                //        ExcelHeaders e2 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[1].ToString());
                                //        ExcelHeaders e3 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[2].ToString());
                                //        ExcelHeaders e4 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[3].ToString());
                                //        ExcelHeaders e5 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[4].ToString());
                                //        ExcelHeaders e6 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[5].ToString());
                                //        ExcelHeaders e7 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[6].ToString());
                                //        ExcelHeaders e8 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[7].ToString());
                                //        ExcelHeaders e9 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[8].ToString());
                                //        ExcelHeaders e10 = (ExcelHeaders)Enum.Parse(typeof(ExcelHeaders), encabezado[9].ToString());

                                //    }
                                //    catch (Exception Error)
                                //    {
                                //        return Json(new { Success = false, Mensaje = "Uno o más encabezados son incorrectos.", Tipo = 0 }, JsonRequestBehavior.AllowGet);
                                //        //tabla.DataWithErrors.Add(new MileageError { Row = i, Details = "Uno o más encabezados son incorrectos." });
                                //    }
                                //}
                            }
                        }

                        else
                        {
                            sinDatos.ForEach(x => { tabla.DataWithErrors.Add(new MileageError { Row = x, Details = "La fila tiene campos vacíos." }); });
                        }
                            
                    }

                    else
                    {
                        return Json(new { Success = false, Mensaje = "¡El archivo está vacío!", Tipo = 0 }, JsonRequestBehavior.AllowGet);
                    }

                    if (tabla.DataWithErrors.Any())
                    {                                   
                        return Json(new { Success = false, Mensaje = "¡Verifica el archivo, no se cuentan con los datos obligatorios!", Tipo = 0, DatosTabla = tabla.DataWithErrors }, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        // Request a la API para validar
                        if(tabla.Data != null)
                        {
                            var client = new RestClient();
                            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["SurveyEngineApiServer"]);
                            int registrosAlmacenados = 0;
                            foreach (var item in tabla.Data)
                            {         
                                var getRequest = new RestRequest("theoreticalMileage", Method.GET);
                                getRequest.AddParameter("BranchId", item.BranchId, ParameterType.QueryString);
                                getRequest.AddParameter("RouteId", item.RouteId, ParameterType.QueryString);
                                getRequest.AddParameter("Travels", item.Travels, ParameterType.QueryString);
                                var RestResponse = client.Execute(getRequest);

                                MileageResponse mileageResponse = JsonConvert.DeserializeObject<MileageResponse>(RestResponse.Content);
                                if (mileageResponse.TotalRecords > 0)
                                {
                                    tabla.DataWithErrors.Add(new MileageError { Row = item.Row, Details = "¡Ya existe un registro con los mismos identificadores!" });                             
                                }

                                else
                                {
                                    // Valida si existe la ruta
                                    var getRouteRequest = new RestRequest("routes/{Id}", Method.GET);
                                    getRouteRequest.AddUrlSegment("Id", item.RouteId.ToString());
                                    var getRouteResponse = client.Execute(getRouteRequest);
                                    JObject resultJson = JsonConvert.DeserializeObject<JObject>(getRouteResponse.Content);

                                    if (getRouteResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                                    {
                                        tabla.DataWithErrors.Add(new MileageError { Row = item.Row, Details = "¡No se encontró la ruta!" });
                                    }
                                    else if((int)resultJson["BranchId"] != item.BranchId)
                                    {
                                        tabla.DataWithErrors.Add(new MileageError { Row = item.Row, Details = "¡La ruta no está asignada al punto de venta especificado!" });
                                    }

                                }
                            }
                            if(tabla.DataWithErrors.Any())
                            {
                                return Json(new { Success = false, Mensaje = "¡Verifica el archivo, algunos datos son erroneos!", Tipo = 0, DatosTabla = tabla.DataWithErrors }, JsonRequestBehavior.AllowGet);
                            }

                            else
                            {
                                foreach(var item in tabla.Data)
                                {
                                    var userPortal = (UserPortal)Session["UserPortal"];
                                    var postRequest = new RestRequest("theoreticalMileage", Method.POST);
                                    postRequest.RequestFormat = DataFormat.Json;
                                    postRequest.AddJsonBody(new
                                    {
                                        BranchId = item.BranchId,
                                        RouteId = item.RouteId,
                                        Travels = item.Travels,
                                        Monday = item.Monday,
                                        Tuesday = item.Tuesday,
                                        Wednesday = item.Wednesday,
                                        Thursday = item.Thursday,
                                        Friday = item.Friday,
                                        Saturday = item.Saturday,
                                        Sunday = item.Sunday,
                                        UserPortalId = userPortal.userPortalId
                                    });

                                    var postResponse = client.Execute(postRequest);

                                    if (postResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        registrosAlmacenados++;
                                    }

                                    //else
                                    //{
                                    //    tabla.DataWithErrors.Add(new MileageError { Row = item.Row, Details = "¡No se encontró la ruta o el punto de venta!" });
                                    //}
                                }

                                if (registrosAlmacenados > 0)
                                {
                                    var getAllRequest = new RestRequest("theoreticalMileage", Method.GET);
                                    var getAllResponse = client.Execute(getAllRequest);
                                    MileageResponse mileageResponse = JsonConvert.DeserializeObject<MileageResponse>(getAllResponse.Content);
                                    var anteriores = mileageResponse.TheoreticalMileages.Where(x => !tabla.Data.Any(y =>
                                        x.BranchId == y.BranchId &&
                                        x.RouteId == y.RouteId &&
                                        x.Travels == y.Travels)).ToList();
                                    anteriores.ForEach(item => { tabla.Data.Add(item); });

                                    return Json(new { Success = true, Mensaje = "¡Registros guardados con éxito!", Tipo = 1, DatosTabla = tabla.Data }, JsonRequestBehavior.AllowGet);
                                }
                            }                                   
                        }
                    }
                }
                else
                {
                    return Json(new { Success = false, Mensaje = "¡El archivo está vacío!", Tipo = 0 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                return Json(new { Success = false, Mensaje = "¡Inténtelo nuevamente!", Tipo = 0 }, JsonRequestBehavior.AllowGet);
            }

            return Json(tabla);
        }


        private List<Closure> getDefaultClosures()
        {
            List<Closure> closures = new List<Closure>();

            Closure closureRoute = new Closure();
            closureRoute.ClosureId = 1;
            closureRoute.ClosureName = "Rutas";

            Closure closureReference = new Closure();
            closureReference.ClosureId = 2;
            closureReference.ClosureName = "Referencias";

            closures.Add(closureRoute);
            closures.Add(closureReference);

            return closures;
        }

        [HttpPost]
        public JsonResult UserListByFiter(int branchId = 0, string filter = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("GetUserWhioutClosureType", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortal.code, branchId = branchId, filter = filter });
                var response = client.Execute(request);
                string content = response.Content;
                Response<List<User>> users = JsonConvert.DeserializeObject<Response<List<User>>>(content);

                return Json(new { Result = "OK", Records = users.Data, TotalRecordCount = users.Data.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private String getContentBranch(int branchId)
        {
            try
            {
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("GetBranchById", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortal.code, branchId = branchId });
                var response = client.Execute(request);
                string content = response.Content;

                return content;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public ContentResult GetClosureByBranchId(int branchId = 0)
        {
            try
            {
                return Content(getContentBranch(branchId));
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        public ContentResult UpdateBranchClosure(int branchId = 0, int closureType = 1)
        {
            try
            {
                var contentBranch = getContentBranch(branchId);
                Response<Branch> branch = JsonConvert.DeserializeObject< Response<Branch>>(contentBranch);
                branch.Data.ClosureTypeId = closureType;
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("SetClosureTypeToBranch", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortal.code, branch = branch.Data});
                var response = client.Execute(request);
                string content = response.Content;

                return Content(content);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        public ContentResult UpdateUserClosure(int userId = 0, string code = "",int closureType = 0)
        {
            try
            {
                User user = new User();
                user.ClosureTypeId = closureType;
                user.code = code;
                user.userId = userId;
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("SetClosureTypeToUsers", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortal.code, user = user });
                var response = client.Execute(request);
                string content = response.Content;

                return Content(content);
            }
            catch (Exception ex)
            {
                
            }
            return Content("");
        }

        [HttpPost]
        public JsonResult UserExceptionListByFiter(int branchId = 0, string filter = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var userPortal = (UserPortal)Session["UserPortal"];
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("GetExceptionsClosureType", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortal.code, branchId = branchId, filter = filter });
                var response = client.Execute(request);
                string content = response.Content;
                Response<List<User>> users = JsonConvert.DeserializeObject<Response<List<User>>>(content);

                return Json(new { Result = "OK", Records = users.Data, TotalRecordCount = users.Data.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
