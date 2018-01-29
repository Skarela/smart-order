<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<siteSmartOrder.Areas.RoutePreparation.Models.Surveys.ApplyAssignedSurvey>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Detalle de Resultado
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-fonts.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-dialog.min.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Css/Bootstrap-carousel.css")%>"/>
    <link rel="stylesheet" href="<%:Url.Content("~/Areas/RoutePreparation/Content/Css/Custom.css")%>"/>

    <div class="row-fluid" style="margin-bottom: 30px;">
        <div class="span8 offset2">
        
            <ul class="nav nav-tabs" id="tabconfig">
                <li><a href="<%:Url.Action("RedirectToView","CoolerConfiguration", new{Area="RoutePreparation"})%>">Revisión de Enfriador</a></li>
                <li><a href="<%:Url.Action("RedirectToView","CoolerConfiguration", new{Area="RoutePreparation"})%>">Revisión de Enfriador</a></li> 
                <li><a href="<%:Url.Action("Index","CoolerConfiguration", new{Area="RoutePreparation"})%>">Nueva Configuracion</a></li>  
                <li><a href="<%:Url.Action("Index","CoolerConfiguration", new{Area="RoutePreparation"})%>">Revisión de Enfriador</a></li>  
                <li><a href="<%:Url.Action("Index","CoolerConfigurationReply", new{Area="RoutePreparation"})%>">Resultados</a></li> 
                <li class="active"><a>Detalle</a></li>
                <li><a href="<%:Url.Action("Index","Customer", new{Area="RoutePreparation"})%>">Clientes</a></li>  
                <li><a href="<%:Url.Action("Index","Contact", new{Area="RoutePreparation"})%>">Contactos</a></li>
                <li><a href="<%:Url.Action("Index","Alert", new{Area="RoutePreparation"})%>">Alertas</a></li>
                <li><a href="<%:Url.Action("New","Contact", new{Area="RoutePreparation"})%>">Nuevo Contacto</a></li>
                <li><a href="<%:Url.Action("New","Alert", new{Area="RoutePreparation"})%>">Nueva Alerta</a></li>           
                <li><a href="<%:Url.Action("Index","CoolerConfigurationTemplate", new{Area="RoutePreparation"})%>">Plantilla</a></li>
                <li><a href="<%:Url.Action("New","CoolerConfigurationTemplate", new{Area="RoutePreparation"})%>">Nueva Plantilla</a></li>          
            </ul>
        
            <h3>Detalle de Resultado <small style="color: gray; font-size: 12px; vertical-align: middle"><%:TempData["CustomerName"]%></small></h3>

            <div class="tab-pane active">
                    <% Html.RenderPartial("_ApplyAssignedSurvey", Model); %>
            </div>
            <a class='btn btn-default' href='<%:Url.Action("Index","CoolerConfigurationReply", new{Area="RoutePreparation"})%>'>Regresar</a>

        </div>
    </div>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-dialog.min.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Components/Bootstrap/Scripts/Bootstrap-carousel.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/General.js")%>" defer></script>
    <script type="text/javascript" src="<%:Url.Content("~/Areas/RoutePreparation/Content/Scripts/Survey/ApplyAssignedSurvey.js")%>" defer></script>

</asp:Content>
