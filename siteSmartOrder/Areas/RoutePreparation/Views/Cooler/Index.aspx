<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<int>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Revisión de Enfriadores
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>
    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li class="active"><a>Revisión de Enfriadores</a></li>
                <li><a href="<%:Url.Action("Index","CoolerConfigurationReply", new{Area="RoutePreparation"})%>">Resultados</a></li> 
                <li><a href="<%:Url.Action("Index","Customer", new{Area="RoutePreparation"})%>">Clientes</a></li>    
                <li><a href="<%:Url.Action("Index","Cooler", new{Area="RoutePreparation"})%>">Enfriadores</a></li>
                <li><a href="<%:Url.Action("Index","Contact", new{Area="RoutePreparation"})%>">Contactos</a></li>
                <li><a href="<%:Url.Action("Index","Alert", new{Area="RoutePreparation"})%>">Alertas</a></li>
                <li><a href="<%:Url.Action("New","Contact", new{Area="RoutePreparation"})%>">Nuevo Contacto</a></li>
                <li><a href="<%:Url.Action("New","Alert", new{Area="RoutePreparation"})%>">Nueva Alerta</a></li>        
            </ul>
        
            <h3>Lista de Enfriadores  <%= Html.ButtonExport() %> </h3>
            <% Html.RenderPartial("_Alerts"); %>

            <div class="tab-pane active">
                <% Html.RenderPartial("_Filters"); %>
                <%= Html.BasicTable(false) %>
            </div>

        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/BootstrapTable/Scripts/BootstrapTable.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Table.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Filters.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/CoolerConfiguration/Index.js")%>" defer></script>

</asp:Content>
