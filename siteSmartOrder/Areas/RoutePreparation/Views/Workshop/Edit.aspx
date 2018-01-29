<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Workshop>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Editar Revisión de Taller
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
            <div class="span12 pagination-right">
                <p  style="text-align: right">
                    Seleccione una sucursal
                </p>
                <%: Html.DropDownList("Branch", new SelectList(new List<string>()), new { style = "width: 220px;" })%>
                <legend></legend>
            </div>
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Workshop", new{Area="RoutePreparation"})%>">Revisiones de Taller</a></li>
                <li><a href="<%:Url.Action("Index","WorkshopReply", new{Area="RoutePreparation"})%>">Resultados</a></li>  
                <li><a href="<%:Url.Action("New","Workshop", new{Area="RoutePreparation"})%>">Nueva</a></li>
                <li class="active"><a>Editar</a></li>           
            </ul>
        
            <h3>Editar Revisión de Taller</h3>
            <% Html.RenderPartial("_Alerts"); %>
            
            <% using (Html.BeginForm("Update", "Workshop", FormMethod.Post, new { id = "Form", enctype = "multipart/form-data" }))
               { %>
                <div class="tab-pane active">
                    <% Html.RenderPartial("_Form", Model); %>
                    <%: Html.Action("_Form", "Survey", new{surveyId=Model.SurveyId}) %>
                </div>
                <%= Html.ButtonsForm(Url.Action("Index", "Workshop", new {Area = "RoutePreparation"}), false) %>
            <% } %>
        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-maxlength.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/moment.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/daterangepicker.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-validator.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-touchspin.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Validator.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Workshop/Form.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/Form.js")%>" defer></script>

</asp:Content>
