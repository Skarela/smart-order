﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Site1.Master" AutoEventWireup="true" CodeBehind="ConcentrateOrdersReport.aspx.cs" Inherits="siteSmartOrder.Reports.ConcentrateOrdersReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Concentrado de Pedidos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
    <tr><td><asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager></td></tr>
        <tr><td><rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="800px">
        </rsweb:ReportViewer></td></tr>
    </table>
</asp:Content>
