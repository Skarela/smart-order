using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using siteSmartOrder.Models;
using Microsoft.Reporting.WebForms;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;

namespace siteSmartOrder.Reports
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var UserPortal = (UserPortal)Session["UserPortal"];
            if (UserPortal != null)
            {
                if (!Page.IsPostBack)
                {
                    this.ScriptManager1.AsyncPostBackTimeout = 36000;
                    var Branch = UserPortal.branch;
                    List<Branch> Branches = new List<Branch>();
                    if (Branch == null)
                        Branches = getBranch(UserPortal);
                    else
                        Branches.Add(Branch);

                    string[] strBranches = Branches.Select(b => b.branchId.ToString()).ToArray();

                    string reportId = Request.QueryString["reportId"];
                    var report = GetReport(Int32.Parse(reportId), UserPortal.code);

                    //var contentReportTitle = (ContentPlaceHolder)this.FindControl("ReportTitle");
                    var label = (Label)this.FindControl("Label1");
                    label.Text = report.description;


                    string sReportServerURL = report.server;//ConfigurationManager.AppSettings["ReportServerURL"].ToString();
                    string sReportPath = report.path;//ConfigurationManager.AppSettings["ReportPath"].ToString() + "/WBC_SO_Rep_Customer_Binnacle_Failed";
                    
                    this.ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(sReportServerURL);
                    this.ReportViewer1.ServerReport.ReportPath = sReportPath;

                    ReportParameter parameter = new ReportParameter("pUserBranch", strBranches, false);
                    this.ReportViewer1.ServerReport.SetParameters(parameter);
                    //this.ReportViewer1.ServerReport.Refresh();
                }
            }
        }

        private siteSmartOrder.Models.Report GetReport(int reportId, string userPortalCode)
        {
            try
            {
                var response = new Response<List<siteSmartOrder.Models.Report>>();
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("GetReport", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { userPortalCode = userPortalCode });
                var r = client.Execute(request);
                string content = r.Content;
                response = JsonConvert.DeserializeObject<Response<List<siteSmartOrder.Models.Report>>>(content);

                if (response.Data.Any())
                    return response.Data.FirstOrDefault(rep => rep.reportId == reportId);
                else
                    return new Models.Report();
            }
            catch (Exception ex)
            {
                return new Models.Report();
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