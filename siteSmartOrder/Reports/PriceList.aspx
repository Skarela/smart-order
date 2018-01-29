<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Site1.Master" AutoEventWireup="true" CodeBehind="PriceList.aspx.cs" Inherits="siteSmartOrder.Reports.PriceList" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, 
    Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lista de Precios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%">
    <tr><td><asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager></td></tr>
        <tr><td><rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="800px">
        </rsweb:ReportViewer></td></tr>
    </table>
</asp:Content>
