<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Checklist>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Nuevo Revisión de Unidad
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Css/daterangepicker.css")%>"/>
    <link rel="stylesheet"href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-touchspin.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        
            <ul class="nav nav-tabs" id="tabconfig">
                 <li><a href="<%:Url.Action("Index","Checklist", new{Area="RoutePreparation"})%>">Revisiones de Unidad</a></li> 
                <li><a href="<%:Url.Action("Index","ChecklistReply", new{Area="RoutePreparation"})%>">Resultados</a></li> 
                <li><a href="<%:Url.Action("New","CheckList", new{Area="RoutePreparation"})%>">Nueva</a></li>           
                <li><a href="<%:Url.Action("Index","CheckListTemplate", new{Area="RoutePreparation"})%>">Plantillas</a></li>          
                <li class="active"><a>Nueva Plantilla</a></li>          
            </ul>
        
            <h3>Plantilla (Nueva)</h3>
            <% Html.RenderPartial("_Alerts"); %>
            
            <% using (Html.BeginForm("Create", "CheckListTemplate", FormMethod.Post, new { id = "Form", enctype = "multipart/form-data" }))
               { %>
                <div class="tab-pane active">
                    <% Html.RenderPartial("_Form", Model); %>
                    <%: Html.Action("_Form", "Survey", new{surveyId=Model.SurveyId}) %>
                </div>
                 <button id='Create' type='submit' class='btn btn-success'>Crear</button>
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
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Validator.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/CheckListTemplate/Form.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/Form.js?v=1.1")%>" defer></script>

</asp:Content>
