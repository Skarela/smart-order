<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Venta
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Reporte de Ventas</h3>
        <iframe src="../../Reports/ConcentrateSalesReport.aspx" width="100%" height="500px"></iframe>
</asp:Content>
