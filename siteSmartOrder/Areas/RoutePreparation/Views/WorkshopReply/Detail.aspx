<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Surveys.ApplyAssignedSurvey>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Detalle de Resultado de Taller
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("Index","Workshop", new{Area="RoutePreparation"})%>">Revisiones de Taller</a></li>
                <li><a href="<%:Url.Action("Index","WorkshopReply", new{Area="RoutePreparation"})%>">Resultados</a></li>
                <li class="active"><a>Detalle</a></li>
                <li><a href="<%:Url.Action("New","Workshop", new{Area="RoutePreparation"})%>">Nueva</a></li>            
            </ul>
        
            <h3>Detalle de Resultado de Taller</h3>

            <div class="tab-pane active">
                    <% Html.RenderPartial("_ApplyAssignedSurvey", Model); %>
            </div>
            <a class='btn btn-default' href='<%:Url.Action("Index","WorkshopReply", new{Area="RoutePreparation"})%>'>Regresar</a>

        </div>
    </div>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/ApplyAssignedSurvey.js")%>" defer></script>

</asp:Content>
