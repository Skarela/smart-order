<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.CoolerConfiguration>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Revisión de Enfriador (Editar)
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

            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","CoolerConfiguration", new{Area="RoutePreparation"})%>">Revisión de enfriadores</a></li>  
                <li><a href="<%:Url.Action("New","CoolerConfiguration", new{Area="RoutePreparation"})%>">Nueva Configuración</a></li>  
                <li><a href="<%:Url.Action("Index","CoolerConfigurationReply", new{Area="RoutePreparation"})%>">Resultados</a></li>  
                <li><a href="<%:Url.Action("Index","Customer", new{Area="RoutePreparation"})%>">Clientes</a></li>  
                <li><a href="<%:Url.Action("Index","Contact", new{Area="RoutePreparation"})%>">Contactos</a></li>
                <li><a href="<%:Url.Action("Index","Alert", new{Area="RoutePreparation"})%>">Alertas</a></li>
                <li><a href="<%:Url.Action("New","Contact", new{Area="RoutePreparation"})%>">Nuevo Contacto</a></li>
                <li><a href="<%:Url.Action("New","Alert", new{Area="RoutePreparation"})%>">Nueva Alerta</a></li>           
                <li><a href="<%:Url.Action("Index","CoolerConfigurationTemplate", new{Area="RoutePreparation"})%>">Plantillas</a></li>
                <li><a href="<%:Url.Action("New","CoolerConfigurationTemplate", new{Area="RoutePreparation"})%>">Nueva Plantilla</a></li> 
                <li class="active"><a>Editar Plantilla</a></li>  
            </ul>
        
            <h3>Plantilla (Editar)</h3>
            <% Html.RenderPartial("_Alerts"); %>
            
            <% using (Html.BeginForm("Update", "CoolerConfigurationTemplate", FormMethod.Post, new { id = "Form", enctype = "multipart/form-data" }))
               { %>
                <div class="tab-pane active">
                    <% Html.RenderPartial("_Form", Model); %>
                    <%: Html.Action("_Form", "Survey", new{surveyId=Model.SurveyId}) %>
                </div>
                <button id='Create' type='submit' class='btn btn-success'>Editar</button>
            <% } %>
        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-maxlength.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/moment.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/daterangepicker.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-validator.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-touchspin.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Selectize/Scripts/Selectize.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Validator.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/CoolerConfigurationTemplate/Form.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/Form.js?v=1.1")%>" defer></script>

</asp:Content>
