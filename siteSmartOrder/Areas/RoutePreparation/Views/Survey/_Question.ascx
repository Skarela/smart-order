<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Question>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Resolvers" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<%
    var questionIndex = ViewData["QuestionIndex"];
    var fieldName = "Questions[" + questionIndex + "]";
    var isRequired = Model.Required ? new { @checked = "checked", @class = "option-required" } : (object)new { @class = "option-required" };
    var type = Model.QuestionType;
%>
<div id="question_<%:questionIndex%>" class="panel panel-default margin-bottom-15 <%:type%> question" data-index="<%:questionIndex%>">
    <%: Html.Hidden(fieldName + ".Id", Model.Id)%>
    <%: Html.Hidden(fieldName + ".QuestionType", Model.QuestionType)%>
    <%: Html.Hidden(fieldName + ".QuestionNumber", Model.QuestionNumber)%>
    <%: Html.Hidden(fieldName + ".QuestionImagesJson", Json.Encode(Model.QuestionImages))%>
    <div class="panel-heading iconPointer collapsible">
        <h5 class="panel-title">
            <span class="collapse-button" data-id="<%:questionIndex %>"><i class="fa fa-chevron-up">
            </i></span><span class="question-number">
                <%: Model.QuestionNumber%></span> .-
            <%: type.ResolverQuestionType()%>
            - <span class="displayText">
                <%: Model.Text %></span> <span class="pull-right remove-question"><i class="fa fa-remove">
                </i></span>    
                <%if (Model.QuestionImages.IsNotEmpty())
      {%>
        <span>
            <div class="btn btn-default btn-mini seeAllImages" data-allimages="<%:Json.Encode(Model.QuestionImages)%>">
                <i class="icon-picture"></i>
            </div>
        </span>
    <%}
      else
      {%>
        <div class="btn btn-mini disabled" style="opacity: 0.60">
            <i class="icon-picture"></i>
        </div>
    <%}%>
        </h5>
    </div>
    <div class="panel-body" style="overflow: hidden">
        <div class="form-horizontal">
            <div class="control-group form-group" style="margin-bottom: 5px">
                <label class="control-label">
                    Pregunta</label>
                <div class="controls">
                    <%: Html.TextBox(fieldName + ".Text", Model.Text, new {@class = "form-control inputName span6", placeholder = "Escriba la pregunta", maxlength = "100", autocomplete = "off" })%>
                    <%: Html.TextBox(fieldName + ".QuestionValue", Model.QuestionValue, new { @class = "form-control questionpoint points span2", placeholder = "Pts", maxlength = "3", autocomplete = "off", onkeypress = "return isNumberKeyUp(event)" })%>
                    <div class="checkbox span2" style="float: none; display: inline-block">
                        <label>
                            <%: Html.CheckBox(fieldName+".Required", Model.Required, isRequired)%>
                            Requerido
                        </label>
                    </div>
                    <span class="inputNameMessage" style="display: none;"><small class="help-block" data-bv-validator="notEmpty" data-bv-for="inputNameMessage">La pregunta es requerida.</small> </span>
                </div>
            </div>
            <div class="control-group form-group" style="margin-bottom: 5px">
                <label class="control-label">
                    Imágenes</label>
                <div class="controls">
                    <div class="buttonsToFiles">
                        <div class="btn btn-primary btn-small btn_uploadFiles">
                            <i class="fa fa-upload"></i>Cargar imágenes
                        </div>
                        <div class=" span8 iconPointer showFiles" style="display: none">
                        </div>
                    </div>
                    <input type="file" multiple="multiple" name="<%:fieldName %>.FileImages" id="<%:fieldName %>.FileImages" class="hide file" />
                </div>
            </div>
            <% if (Model.QuestionType == (int)QuestionType.MultipleChoice)
               {%>
            <div class="control-group form-group input-prepend numberRequired" style="margin-bottom: 5px">
                <label for="<%:fieldName %>.AnswerRequiredNumber" class="control-label">
                    Respuestas requeridas</label>
                <div class="controls">
                    <%: Html.TextBox(fieldName + ".AnswerRequiredNumber", Model.AnswerRequiredNumber, new
                            {
                                @class="form-control touchspin span3",
                                placeholder="1",
                                @style = "border-radius:0px !important;"
                            })  %>
                </div>
            </div>
            <%}%>
            <% if (Model.QuestionType == (int)QuestionType.Dichotomy || Model.QuestionType == (int)QuestionType.MultipleChoice)
               { %>
            <legend></legend>
            <div class="options-container">
                <%
                   var answerIndex = 0;
                   foreach (var answer in Model.Answers)
                   {%>
                <%: Html.Action("_Answer", "Survey", new { answer, Model.QuestionType, questionIndex, answerIndex })%>
                <%answerIndex++;
                   } %>
            </div>
            
            <% if (Model.QuestionType == (int)QuestionType.MultipleChoice)
               { %>
            <div class="control-group form-group">
                <label class="control-label">
                </label>
                <div class="controls">
                    <span class="AnswerMessage" style="display: none;"><small class="help-block"
                        data-bv-validator="notEmpty" data-bv-for="AnswerMessage">Agregar al menos una opción.</small>
                    </span>
                    <button type="button" class="btn btn-primary add-option btn-small" data-question="<%:questionIndex %>"
                        data-type="<%: type %>">
                        <i class="fa fa-plus"></i>Agregar Opción</button>
                </div>
            </div>
            <% }%>
            <% }%>
        </div>
    </div>
</div>
