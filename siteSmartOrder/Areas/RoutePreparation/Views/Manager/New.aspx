<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Manager>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Nuevo Contacto
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Css/daterangepicker.css")%>"/>
    <link rel="stylesheet"href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-touchspin.min.css")%>"/>
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
                <li><a href="<%:Url.Action("Index","SosAlert", new{Area="RoutePreparation"})%>">Sos</a></li>
                <li><a href="<%:Url.Action("Index","Incident", new{Area="RoutePreparation"})%>">Tipos de siniestros</a></li> 
                <li><a href="<%:Url.Action("Index","Manager", new{Area="RoutePreparation"})%>">Contactos</a></li> 
                <li class="active"><a>Nuevo Contacto</a></li>           
            </ul>
        
            <h3>Nuevo Contacto</h3>
            <% Html.RenderPartial("_Alerts"); %>
            
            <% using (Html.BeginForm("Create", "Manager", FormMethod.Post, new { id = "Form", enctype = "multipart/form-data" }))
               { %>
                <div id="CampaignContent">
                    <div class="tab-pane active">
                       <% Html.RenderPartial("_Form", Model); %>
                    </div>
                </div>
                <%= Html.ButtonsForm(Url.Action("Index", "Manager", new {Area = "RoutePreparation"}), true) %>
            <% } %>
        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-maxlength.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-phone.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-validator.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-touchspin.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/moment.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/daterangepicker.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Selectize/Scripts/Selectize.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Validator.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Manager/Form.js")%>" defer></script>

</asp:Content>
