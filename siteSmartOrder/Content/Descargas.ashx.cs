using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Content
{
    /// <summary>
    /// Descripción breve de Descargas
    /// </summary>
    public class Descargas : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            string app = request["app"];
            switch(Convert.ToInt32(app))
            {
                case 1:
                    app = "workbycloudso.apk";
                    break;
                case 2:
                    app = "credit.apk";
                    break;
                case 3:
                    app = "svd.apk";
                    break;
                case 4:
                    app = "SurveyWBC.apk";
                    break;
                default:
                    app = null;
                    break;
            }
           
            if(app!= null)
            {
                string path = ConfigurationManager.AppSettings["Applications"];
                context.Response.Clear();
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + app);
                context.Response.WriteFile(path + app);
                context.Response.End();
            }else
            {
                
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}