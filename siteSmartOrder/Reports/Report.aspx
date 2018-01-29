<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="siteSmartOrder.Reports.Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>
    Reportes WBC
    </title>
    <style type="text/css">
        html, body
        {
            font-family: "Helvetica Neue", Helvetica, Arial, Sans-Serif;
        }
        #header
        {
            background-color: #002D45;
            -webkit-box-shadow: 0 3px 2px 0px rgba(0, 0, 0, 0.3);
            -moz-box-shadow: 0 3px 2px 0px rgba(0, 0, 0, 0.3);
            box-shadow: 0 3px 2px 0px rgba(0, 0, 0, 0.3);
            background-image: -webkit-linear-gradient(top, #002D45, #001D2C);
            background-image: -moz-linear-gradient(top, #002D45, #001D2C);
            background-image: linear-gradient(top, #002D45, #001D2C);
        }
    </style>
</head>
<body style="padding: 0px; margin: 0px; background-color: #E3E3E3; height:100%; overflow-y:hidden; border: 1px none green">
    <div id="header" style=" color: white; text-align: left; font-size: x-large; height:8%;">
        <table width="100%" style="text-align:inherit;">
            <tr>
                <td>
                    <img id="logoOpe" src="<%:ResolveClientUrl("~/Content/bootstrap/img/logo18_.png")%>" 
                    style="margin-left: -50px; width:200px;" />
                </td>
                <td style="margin-bottom:5px;">
                    <asp:label id="Label1" runat="server" style="color:White; font-size: large;"> REPORTE </asp:label>
                </td>
            </tr>
        </table>  
    </div>
    <div style="height:90%; ">
    <form id="form1" runat="server" style="border:1px none red; height:99%">
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" style="border: 1px none blue" Height="100%">
        </rsweb:ReportViewer>
    </form>
    </div>
</body>
</html>