<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Campaign>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Editar Plantilla de Concientización
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Css/Messenger.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Css/daterangepicker.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/MultimediaModule.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href ="<%:Url.Action("Index" ,"Campaign", new{Area="RoutePreparation"})%>">Campañas</a></li>
                <li><a href="<%:Url.Action("Index","CampaignReply", new{Area="RoutePreparation"})%>">Resultados</a></li> 
                <li><a href="<%:Url.Action("New","Campaign", new{Area="RoutePreparation"})%>">Nueva</a></li>           
                <li><a href="<%:Url.Action("Index" ,"CampaignTemplate", new{Area="RoutePreparation"})%>">Plantillas</a></li>
                <li><a href ="<%:Url.Action("New" ,"CampaignTemplate", new{Area="RoutePreparation"})%>">Nueva Plantilla</a></li>
                <li class="active"><a>Editar Plantilla</a></li>
            </ul>
        
            <h3>Editar Plantilla</h3>
            <% Html.RenderPartial("_Alerts"); %>
            
            <% using (Html.BeginForm("Update", "CampaignTemplate", FormMethod.Post, new { id = "Form", enctype = "multipart/form-data" }))
               { %>
                <div id="CampaignContent">
                    <div class="tab-pane active">
                       <% Html.RenderPartial("_Form", Model); %>
                       <% Html.RenderPartial("~/Areas/RoutePreparation/Views/Campaign/_Multimedia.ascx", Model); %>
                       <%: Html.Action("_Form", "survey", new { surveyId = Model.SurveyId })%>
                    </div>
                </div>
                <%= Html.ButtonsForm(Url.Action("Index", "CampaignTemplate", new {Area = "RoutePreparation"}), false) %>
            <% } %>
        </div>
    </div>
    <% Html.RenderPartial("_UrlActions"); %>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Messenger/Scripts/Messenger.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-maxlength.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/moment.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/DateRangePicker/Scripts/daterangepicker.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-validator.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-touchspin.min.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Selectize.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Validator.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/CampaignTemplate/Form.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/Form.js?v=1.1")%>" defer></script>    
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaModalModule.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaModule.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaModalFilter.js?v=1.1")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Campaign/MultimediaFileUploader.js?v=1.1")%>" defer></script>

</asp:Content>
