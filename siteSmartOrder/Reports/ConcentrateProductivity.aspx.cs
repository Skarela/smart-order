using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace siteSmartOrder.Reports
{
    public partial class ConcentrateProductivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             var UserPortal = Session["UserPortal"];
             if (UserPortal != null)
             {
                 if (!Page.IsPostBack)
                 {
                     string sReportServerURL = ConfigurationManager.AppSettings["ReportServerURL"].ToString();
                     string sReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString() + "/WBC_SO_Rep_Productivity";

                     this.ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                     this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(sReportServerURL);
                     this.ReportViewer1.ServerReport.ReportPath = sReportPath;

                     //this.ReportViewer1.ServerReport.Refresh();
                 }
             }
        }
    }
}