﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Campaign>" %>

<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Nueva Campaña de Concientización
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Css/daterangepicker.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-touchspin.min.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/MultimediaModule.css")%>" />
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/MultiSelectize/sol.css")%>" />
    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
            <div class="span12 pagination-right" style="display: none">
                <p style="text-align: right">
                    Seleccione una sucursal
                </p>
                <%: Html.DropDownList("Branch", new SelectList(new List<string>()), new { style = "width: 220px;" })%>
                <legend></legend>
            </div>
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Campaign", new{Area="RoutePreparation"})%>">Campañas</a></li>
                <li><a href="<%:Url.Action("Index","CampaignReply", new{Area="RoutePreparation"})%>">
                    Resultados</a></li>
                <li class="active"><a>Nueva</a></li>
                <li><a href="<%:Url.Action("Index" ,"CampaignTemplate", new{Area="RoutePreparation"})%>">
                    Plantillas</a></li>
                <li><a href="<%:Url.Action("New" ,"CampaignTemplate", new{Area="RoutePreparation"})%>">
                    Nueva Plantilla</a></li>
            </ul>
            <h3>
                Nueva campaña</h3>
            <% Html.RenderPartial("_Alerts"); %>
            <% using (Html.BeginForm("Create", "Campaign", FormMethod.Post, new { id = "Form", enctype = "multipart/form-data" }))
               { %>
            <div id="CampaignContent">
                <div class="tab-pane active">
                    <% Html.RenderPartial("_Form", Model); %>
                    <% Html.RenderPartial("_Branches", Model); %>
                    <% Html.RenderPartial("_Dates", Model); %>
                    <% Html.RenderPartial("_Templates", Model); %>
                    <div id="multimeda-container">
                        <% Html.RenderPartial("_Multimedia", Model); %>
                    </div>
                    <div id="survey-container">
                        <%: Html.Action("_Form", "Survey", new { surveyId = Model.SurveyId })%>
                    </div>
                </div>
            </div>
            <%= Html.ButtonsForm(Url.Action("Index", "Campaign", new { Area = "RoutePreparation" }), true)%>
            <% } %>
        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-maxlength.min.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/moment.min.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/daterangepicker.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-validator.min.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-touchspin.min.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Validator.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/Form.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/New.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/Form.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/Dates.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaModalModule.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaModule.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaModalFilter.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaFileUploader.js?v=1.1")%>"
        defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/MultiSelectize/sol.js?v=1.1")%>"
        defer></script>
</asp:Content>
