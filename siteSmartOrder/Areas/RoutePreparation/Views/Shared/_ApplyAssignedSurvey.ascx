<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Surveys.ApplyAssignedSurvey>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Resolvers" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<% var survey = Model.AssignedSurvey.Survey; %>
<div class="panel panel-default margin-bottom-15">
    <div class="panel-heading iconPointer collapsible">
        <h5 class="panel-title">
            <span class="collapse-button"><i class="fa fa-chevron-up"></i></span>Información
            general
        </h5>
    </div>
    <div class="panel-body" style="overflow: hidden; padding: 0;">
        <table class="table" style="margin-bottom: 0 !important;">
            <tbody>
                <tr>
                    <td class="table-label">
                        <label class="control-label ">
                            Título</label>
                    </td>
                    <td class="table-value">
                        <label class="control-label">
                            <%: survey.Name%></label>
                    </td>
                </tr>
                <tr>
                    <td class="table-label">
                        <label class="control-label">
                            Categoría</label>
                    </td>
                    <td class="table-value">
                        <label class="control-label">
                            <%: survey.Category.Name%></label>
                    </td>
                </tr>
                <tr>
                    <td class="table-label">
                        <label class="control-label">
                            Fecha de creación</label>
                    </td>
                    <td class="table-value">
                        <label class="control-label">
                            <%: Model.ClientDate%></label>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="table table-striped" style="margin-bottom: 0; border-top: 1px solid #dddddd;">
            <thead>
                <tr>
                    <td style="background-color: #f5f5f5; text-align: center; width: 2%;">
                        <b>Num.</b>
                    </td>
                    <td style="background-color: #f5f5f5; text-align: center; width: 40%;">
                        <b>Pregunta</b>
                    </td>
                    <td style="background-color: #f5f5f5; text-align: center; width: 10%;">
                        <b>Imágenes</b>
                    </td>
                    <td style="background-color: #f5f5f5; text-align: center; width: 15%;">
                        <b>Tipo</b>
                    </td>
                    <td style="background-color: #f5f5f5; text-align: center; width: 30%;">
                        <b>Respuesta</b>
                    </td>
                </tr>
            </thead>
            <tbody>
                <% foreach (var question in survey.Questions.OrderBy(q => q.QuestionNumber))
                   {
                       var required = question.Required ? " *" : "";
                       var questionResult = "--";
                %>
                <tr>
                    <td style="text-align: center;">
                        <%:question.QuestionNumber %>
                    </td>
                    <td style="text-align: center;">
                        <%:question.Text %><b style="color: red; font-size: 17px"><%:required %></b>
                    </td>
                    <td style="text-align: center;">
                        <%if (question.QuestionImages.IsNotEmpty())
                          {%>
                        <div class="btn btn-mini seeAllImages" data-allimages="<%:Json.Encode(question.QuestionImages)%>">
                            <i class="icon-picture"></i>
                        </div>
                        <%}
                          else
                          {%>
                        <div class="btn btn-mini disabled" style="opacity: 0.60">
                            <i class="icon-picture"></i>
                        </div>
                        <% }%>
                    </td>
                    <td style="text-align: center;">
                        <%:question.QuestionType.ResolverQuestionType() %>
                    </td>
                    <td style="text-align: center;">
                        <% if (question.QuestionType == (int)QuestionType.Text || question.QuestionType == (int)QuestionType.Numeric)
                           {
                               var answerResult = Model.AssignedSurveyResults.FirstOrDefault(m => m.QuestionId == question.Id && m.AnswerId == 0);
                               if (answerResult.IsNotNull())
                               {
                                   questionResult = answerResult.TextResult;
                        %>
                        <table class="table table-bordered table-striped" style="margin-bottom: 0;">
                            <tr>
                                <td style="width: 100%;">
                                    <%:questionResult %>
                                </td>
                            </tr>
                        </table>
                        <% }
                           }
                           else if (question.QuestionType == (int)QuestionType.Dichotomy || question.QuestionType == (int)QuestionType.MultipleChoice)
                           {%>
                        <table class="table table-bordered table-striped" style="margin-bottom: 0;">
                            <% foreach (var answer in question.Answers)
                               {
                                   var answerResult = Model.AssignedSurveyResults.FirstOrDefault(m => m.AnswerId == answer.Id);
                                   if (answerResult.IsNotNull())
                                   {
                                       questionResult = answerResult.TextResult;
                            %>
                            <tr>
                                <td style="width: 80%;">
                                    <%:questionResult%>
                                </td>
                            </tr>
                            <% }
                               }%>
                        </table>
                        <%}
                           else if (question.QuestionType == (int)QuestionType.Photo)
                           {
                               var answerResult = Model.AssignedSurveyResults.FirstOrDefault(m => m.QuestionId == question.Id && m.AnswerId == 0);
                               var labelResult = answerResult.AssignedSurveyResultImages.Count.IsEqualTo(1)  ?  "imagen" :"imágenes";
                        %>
                                                  <table class="table table-bordered table-striped" style="margin-bottom: 0;">
                            <tr>
                        <%if (answerResult.AssignedSurveyResultImages.IsNotEmpty())
                          { %>
                        <td style="width: 90%;">
                            <%:answerResult.AssignedSurveyResultImages.Count%>
                            <%:labelResult%>
                        </td>
                        <td style="width: 10%;">
                            <div class="btn btn-default btn-mini seeAllImages" data-allimages="<%:Json.Encode(answerResult.AssignedSurveyResultImages)%>">
                                <i class="icon-picture"></i>
                            </div>
                        </td>

                        <%}
                          else
                          { %>
                        <td style="width: 10%;">
                            <div class="btn btn-mini disabled" style="opacity: 0.60">
                                <i class="icon-picture"></i>
                            </div>
                        </td>

                        <% } %>
                        
                            </tr>
                        </table>
                        <% } %>
                    </td>
                </tr>
                <% } %>
            </tbody>
            <tr>
                <td colspan="6" style="background: white">
                    <b style="float: right; margin-right: 5%">Puntaje Total:
                        <%: Model.AssignedSurveyResults.Sum(asr => asr.AnswerPoint) %></b>
                </td>
            </tr>
        </table>
    </div>
</div>
