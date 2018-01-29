<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Answer>" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<%
    var questionIndex = ViewData["QuestionIndex"];
    var answerIndex = ViewData["AnswerIndex"];
    var answerNumber = ViewData["AnswerNumber"];
    var questionType = ViewData["QuestionType"];
    var fieldName = "Questions[" + questionIndex + "].Answers[" + answerIndex + "]";
    var classFaToggle = Model.AnswerPoint.IsGreaterThanZero() ? "fa-toggle-on" : "fa-toggle-off";
    var classFaBell = Model.AlertId.IsGreaterThanZero() ? "fa-bell" : "fa-bell-o";
    var attrDisabled = (int)questionType == (int)QuestionType.Dichotomy ? new { @readonly = "readonly", @class = "form-control inputAnswerText span6", placeholder = "Opción" } : (object)new { @class = "form-control inputAnswerText span6", placeholder = "Opción" };
%>
<div class="control-group form-group answer">
    <%: Html.Hidden(fieldName + ".Id", Model.Id)%>
    <label class="control-label">
        Opción
        <%:answerNumber%></label>
    <div class="controls">
        <%: Html.TextBox(fieldName + ".Text", Model.Text, attrDisabled)%>
        <%: Html.TextBox(fieldName + ".AnswerPoint", Model.AnswerPoint, new { @class = "form-control answerpoint points span2", placeholder = "Pts", maxlength = "3", autocomplete = "off", onkeypress = "return isNumberKeyUp(event)" })%>
        <%: Html.TextBox(fieldName + ".AlertId", Model.AlertId, new { @class = "hide alertConfiguration" })%>
        <div style="display:none;"><button type="button" class="btn btn-success btn-small" data-toggle="button" aria-pressed="false" autocomplete="off" >Bien</button></div>
        <button style="display:none;" type="button" class="btn btn-small btn-hasPoint notEvent"><i class="fa <%:classFaToggle %> "></i></button>
        
        <% if ((int)questionType == (int)QuestionType.MultipleChoice || (int)questionType == (int)QuestionType.Dichotomy)
           { %>
            <div class="btn btn-default btn-alert" style="float: none; display: none; margin-left: 10px;">
            <i class="fa <%:classFaBell %>"></i>
        </div>
        <% } %>
       
        <% if ((int)questionType == (int)QuestionType.MultipleChoice)
           { %>
        <div class="btn btn-danger remove-option" style="float: none; display: inline-block;
            margin-left: 10px;">
            <i class="fa fa-remove"></i>
        </div>
        <span class="inputAnswerTextMessage" style="display: none;"><small class="help-block"
            data-bv-validator="notEmpty" data-bv-for="inputAnswerTextMessage">La Opción es requerida.</small>
        </span>
        <% } %>
    </div>
</div>
