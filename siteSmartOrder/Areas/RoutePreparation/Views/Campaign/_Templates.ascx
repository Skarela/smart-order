<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Campaign>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Models.ViewModels" %>
<% var templates = ViewData["Templates"] as List<TemplateCampaignViewModel>;
   var controllerName = ViewData["ControllerName"] as string;
   var area = "RoutePreparation";
%>
<script>

    var allTemplatesJson = '<%: Html.Raw( Json.Encode(templates))%>';
    $(document).ready(function () {
        $('#applyTemplate').click(function () {
           
            var multimediaUri ="<%: Url.Action("_AsignMultimediaTemplate", "Campaign", new {Area = area}) %>";
            var selectedCampaingid = $('#selectTemplates').val();
            var multimediaData = { campaignId: selectedCampaingid };
            function multimediaCallback() {                
                multimediaModalModuleFunct(jQuery, window);
                multimediaModuleFunct(jQuery, window);
                multimediaModalFilterFunct(jQuery, window);
                multimediaFileUploaderFunct(jQuery, window);
            }
            $("#multimeda-container").empty();
            appendPartialView("multimeda-container", multimediaUri, multimediaData, multimediaCallback);


            var templateDataList = JSON.parse(allTemplatesJson);
            var selectedTemplate = $.grep(templateDataList, function (item) {
                return item.Id == selectedCampaingid;
            })[0];


            var SurveyUri = "<%: Url.Action("_Form", "Survey", new {Area = area}) %>";
           
            var surveyData = { surveyId: selectedTemplate.SurveyId };

            function surveyCallback(){
                loadSurvey();
                changeNameAndDescription();
                $("#SurveyName").val($("#Name").val());
                $("#SurveyDescription").val($("#Description").val());
            }
            $("#survey-container").empty();
            appendPartialView("survey-container", SurveyUri, surveyData, surveyCallback);
            
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
                <button id='applyTemplate' type='button' class='btn btn-success' name="applyTemplate"
                    value="template">
                    Aplicar</button>
            </div>
        </div>
    </div>
</div>
