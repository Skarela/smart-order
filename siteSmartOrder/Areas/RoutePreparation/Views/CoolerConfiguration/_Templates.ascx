<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.CoolerConfiguration>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Models.ViewModels" %>
<% var templates = ViewData["Templates"] as List<TemplateViewModel>;
   var controllerName= ViewData["ControllerName"] as string;
   var area = "RoutePreparation";
       %>

<script>
   

    $(document).ready(function () {
        $('#applyTemplate').click(function () {
            var uri = "<%: Url.Action("_Form", "Survey", new {Area = area}) %>";
            var surveyData = { surveyId: $('#selectTemplates').val() };
            $("#survey-container").empty();
            appendPartialView("survey-container", uri, surveyData, loadSurvey);

        });
    });
    

</script>

<div class="panel-title">
    Plantilla
</div>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <%: Html.Label("Nombre", new {@class = "control-label"})%>
            <div class="controls">
                <%: Html.DropDownList("selectTemplates", new SelectList(templates, "Id", "Name"), new { @class = "span7" })%>
                <button id='applyTemplate' type='button' class='btn btn-success' name="applyTemplate" value="template">Aplicar</button>

            </div>
        </div>
    </div>
</div>


