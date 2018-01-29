using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Newtonsoft.Json;
using siteSmartOrder.Models;

namespace siteSmartOrder.Content
{
    /// <summary>
    /// Descripción breve de QRCobratario
    /// </summary>
    public class QRCobratario : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context) 
        {
            context.Response.ContentType = "images/jpg";
            int size = 390;
            var userPortal = (UserPortal)HttpContext.Current.Session["UserPortal"];

            string userCode = context.Request.QueryString["userCode"];
            string userName = context.Request.QueryString["userName"];
            string branchCode = context.Request.QueryString["branchCode"];
            string branchName = context.Request.QueryString["branchName"];
                                                            
            var info =  new {
                Service = ConfigurationManager.AppSettings["Service"],
                ServiceName = ConfigurationManager.AppSettings["ServiceName"],

                UserCode = userCode,
                UserName = userName,
                
                BranchCode = branchCode,
                BranchName= branchName
            };
            Bitmap img = OpeSystems.QRCode.encode(JsonConvert.SerializeObject(info), size, size, new Bitmap(1,1), Color.Empty);// OpeSystems.QRCode.encode(js.Serialize(info), size, size);
            img.Save(context.Response.OutputStream,
                System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            
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