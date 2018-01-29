<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lista de Alertas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Selectize/Css/Selectize.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
            <div class="span12 pagination-right">
                <p  style="text-align: right">
                    Seleccione una sucursal
                </p>
                <%: Html.DropDownList("Branch", new SelectList(new List<string>()), new { style = "width: 220px;" })%>
                <legend></legend>
            </div>
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li class="active"><a>Sos</a></li>
                <li><a href="<%:Url.Action("Index","Incident", new{Area="RoutePreparation"})%>">Tipos de siniestros</a></li> 
                <li><a href="<%:Url.Action("Index","Manager", new{Area="RoutePreparation"})%>">Contactos</a></li> 
                <li><a href="<%:Url.Action("New","Manager", new{Area="RoutePreparation"})%>">Nuevo Contacto</a></li>   
            </ul>
        
            <h3>Lista de Alertas  <%= Html.ButtonExport() %> </h3>
            <% Html.RenderPartial("_Alerts"); %>

            <div class="tab-pane active">
                <div class="alert-map" id="Map_canvas"></div>
                <% Html.RenderPartial("_Filters"); %>
                <%= Html.BasicTable(false) %>
            </div>

        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%= Html.UrlGoogleMaps() %>"></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/BootstrapTable/Scripts/BootstrapTable.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Selectize/Scripts/Selectize.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/InfoBubble/Scripts/InfoBubble.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Map.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Table.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Filters.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/SosAlert/Index.js")%>" defer></script>

</asp:Content>
