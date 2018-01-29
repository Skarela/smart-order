<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Plantillas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Css/daterangepicker.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href ="<%:Url.Action("Index" ,"Campaign", new{Area="RoutePreparation"})%>">Campañas</a></li>
                <li><a href="<%:Url.Action("Index","CampaignReply", new{Area="RoutePreparation"})%>">Resultados</a></li> 
                <li><a href="<%:Url.Action("New","Campaign", new{Area="RoutePreparation"})%>">Nueva</a></li>           
                <li class="active"><a>Plantillas</a></li>
                <li><a href ="<%:Url.Action("New" ,"CampaignTemplate", new{Area="RoutePreparation"})%>">Nueva Plantilla</a></li>
            </ul>
        
            <h3>Lista de plantillas </h3>
            <% Html.RenderPartial("_Alerts"); %>

            <div class="tab-pane active">
                <% Html.RenderPartial("_Filters"); %>
                <%= Html.BasicTable(false) %>
            </div>

        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/BootstrapTable/Scripts/BootstrapTable.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/moment.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/daterangepicker.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Filters.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Table.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/CampaignTemplate/Index.js?v=1.1")%>" defer></script>

</asp:Content>
