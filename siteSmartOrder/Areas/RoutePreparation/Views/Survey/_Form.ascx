<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Survey>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Resolvers" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<% var questionTypes = new QuestionType().ConvertToCollection(); %>
<div class="panel-title">
    Encuesta
</div>
<div class="panel-body" style="overflow: hidden" id="SurveyForm">
    <%: Html.HiddenFor(model => model.Id, new {id="ValidSurveyId"} )%>
    <%: Html.HiddenFor(model => model.Status)%>
    <%: Html.Hidden("Category.Id", Model.Category.Id)%>
    <div class="span8">
        <div class="form-horizontal">
            <div class="control-group form-group">
                <%: Html.LabelFor(model => model.Name, "Nombre", new {@class = "control-label"})%>
                <div class="controls">
                    <%: Html.TextBoxFor(model => model.Name, new { id = "SurveyName", @class = "span10", placeholder = "Nombre", maxlength = "60", autocomplete = "off" })%>
                    <span id="SurveyNameMessage" style="display: none;"><small class="help-block" data-bv-validator="notEmpty"
                        data-bv-for="SurveyNameMessage">El Nombre es requerido.</small> </span>
                </div>
            </div>
            <div class="control-group form-group">
                <%: Html.LabelFor(model => model.Description, "Descripción", new { @class = "control-label" })%>
                <div class="controls">
                    <%: Html.TextAreaFor(model => model.Description, new {id="SurveyDescription", @class = "span10", @style = "resize:vertical;", placeholder = "Descripción", maxlength = "120", autocomplete = "off" })%>
                </div>
            </div>
        </div>
    </div>
    <div class="span3" id="AditionalOptions">
        <label>
            <strong>Opciones Adicionales</strong></label>
        <div class="checkbox">
            <label for="Weighted" class="iconPointer">
                <%:Html.CheckBoxFor(m => m.Weighted)%>
                Agregar puntaje
            </label>
        </div>
        <div class="checkbox points">
            <label for="ShowPoints" class="iconPointer">
                <%:Html.CheckBoxFor(m =>m.ShowPoints)%>
                Mostrar puntaje en móvil
            </label>
        </div>
    </div>
    <legend></legend>
    <div id="questions-container">
        <%
            var index = 0;
            foreach (var question in Model.Questions)
            {%>
        <%: Html.Action("_Question", "Survey", new { question, index })%>
        <%index++;
            } %>
    </div>
    <div style="margin: 5px -10px -10px; padding: 15px;  background-color: #E2E2E2;">
        <strong>
            <label style="display: initial">
                Agregar pregunta:</label></strong>
        <div class="btn-group">
            <% foreach (var questionType in questionTypes)
               {%>
                   <button type="button" class="btn btn-primary btn-small add-question" data-type="<%:questionType.Value%>">
                <%:questionType.Value.ResolverQuestionType()%></button>
               <% } %>

        </div>
        <span id="QuestionMessage" style="display: none;" class="has-error"><small class="help-block"
            style="margin-left: 20%" data-bv-validator="notEmpty" data-bv-for="QuestionMessage">
            Agregar al menos una pregunta.</small> </span>
    </div>
</div>
