using System;
using System.Collections.Generic;
using System.Web.Mvc;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Areas.RoutePreparation.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyRepository;
        private readonly IJsonFactory _jsonFactory;

        public SurveyController(ISurveyService surveyRepository, IJsonFactory jsonFactory)
        {
            _surveyRepository = surveyRepository;
            _jsonFactory = jsonFactory;
        }

        #region Get Request

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public PartialViewResult _Form(int surveyId)
        {
            try
            {

                var survey = new Survey();
                if (surveyId.IsGreaterThanZero())
                {
                    survey = _surveyRepository.Get(surveyId);
                    survey = _surveyRepository.GetLastActive(survey.Code);
                }
                return PartialView("_Form", survey);
            }
            catch
            {
                return PartialView("Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public PartialViewResult _Question(Question question, int index)
        {
            ViewData["QuestionIndex"] = index;
            question.QuestionNumber = index + 1;
            if (question.QuestionType.IsEqualTo((int)QuestionType.Dichotomy) && question.Id.IsEqualToZero())
                question.Answers.AddRange(new List<Answer> { new Answer { Text = "Si" }, new Answer { Text = "No" } });
            if (question.QuestionType.IsEqualTo((int)QuestionType.MultipleChoice) && question.Id.IsEqualToZero())
                question.Answers.AddRange(new List<Answer> { new Answer() });
            return PartialView("_Question", question);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public PartialViewResult _Answer(Answer answer, int questionType, int questionIndex, int answerIndex)
        {
            ViewData["QuestionType"] = questionType;
            ViewData["QuestionIndex"] = questionIndex;
            ViewData["AnswerIndex"] = answerIndex;
            ViewData["AnswerNumber"] = answerIndex + 1;
            return PartialView("_Answer", answer);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Get(int id)
        {
            try
            {
                var campaign = _surveyRepository.GetFlat(id);
                return _jsonFactory.Success(campaign);
            }
            catch (Exception e)
            {
                return _jsonFactory.Failure(e.Message, e.GetType());
            }
        }

        #endregion

    }
}
