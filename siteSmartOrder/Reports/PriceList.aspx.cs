using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using siteSmartOrder.Models;
using Microsoft.Reporting.WebForms;
using RestSharp;
using Newtonsoft.Json;

namespace siteSmartOrder.Reports
{
    public partial class PriceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var UserPortal = (UserPortal)Session["UserPortal"];
            if (UserPortal != null)
            {
                if (!Page.IsPostBack)
                {
                    var Branch = UserPortal.branch;
                    List<Branch> Branches = new List<Branch>();
                    if (Branch == null)
                        Branches = getBranch(UserPortal);
                    else
                        Branches.Add(Branch);

                    string[] strBranches = Branches.Select(b => b.branchId.ToString()).ToArray();

                    string sReportServerURL = ConfigurationManager.AppSettings["ReportServerURL"].ToString();
                    string sReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString() + "/WBC_SO_Rep_PriceList";

                    this.ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(sReportServerURL);
                    this.ReportViewer1.ServerReport.ReportPath = sReportPath;

                    ReportParameter parameter = new ReportParameter("pUserBranch", strBranches, false);
                    this.ReportViewer1.ServerReport.SetParameters(parameter);
                    //this.ReportViewer1.ServerReport.Refresh();
                }
            }
        }

        private List<Branch> getBranch(UserPortal UserPortal)
        {
            var branches = new List<Branch>();
            var client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
            var request = new RestRequest("Branch/All", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { code = UserPortal.code });
            var response = client.Execute(request);
            string content = response.Content;
            branches = JsonConvert.DeserializeObject<List<Branch>>(content);
            return branches;
        }
    }
}