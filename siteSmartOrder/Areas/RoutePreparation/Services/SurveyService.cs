using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using RestSharp;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Pages;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Requests;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Settings;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Areas.RoutePreparation.Services
{
    public class SurveyService : ISurveyService
    {
        private IClient _client;
        private readonly IAlertConfigurationService _alertConfigurationService;

        public SurveyService(IAlertConfigurationService alertConfigurationService)
        {
            _alertConfigurationService = alertConfigurationService;
        }

        public Survey Get(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys/{0}", id);
            var response = _client.Get<SurveyPage>(uri);
            var survey = response.Surveys.FirstOrDefault();
            SetAlertIds(survey);
            return survey;
        }
        public Survey GetPlain(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys/plain?surveyId={0}", id);
            var response = _client.Get<SurveyPage>(uri);
            var survey = response.Surveys.FirstOrDefault();
            SetAlertIds(survey);
            return survey;
        }
        public Survey GetLastActive(string code)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveycode/{0}", code);
            var response = _client.Get<SurveyPage>(uri);
            var survey = response.Surveys.FirstOrDefault();
            SetAlertIds(survey);
            return survey;
        }

        public SurveyFlat GetFlat(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys/{0}", id);
            var response = _client.Get<SurveyFlatPage>(uri);
            return response.Surveys.FirstOrDefault();
        }

        public SurveyPage Filter(SurveyFilter request)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys");
            return _client.Filter<SurveyPage>(uri, request);
        }

        public void Create(Survey survey)
        {
            var jss = new JavaScriptSerializer();
            //TODO: make this more elegant
            foreach (var question in survey.Questions)
            {
                try
                {

                    if (question.QuestionImagesJson.IsNotNullOrEmpty())
                    {
                        question.QuestionImages = jss.Deserialize<List<QuestionImage>>(question.QuestionImagesJson);
                    }

                }
                catch (Exception)
                {
                }
            }

            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys");
            var request = new SurveyPage { Surveys = new List<Survey> { survey } };
            var response = _client.Post(uri, request);
            var surveyCreated = response.Surveys.First();
            survey.Id = surveyCreated.Id;
            ChangeStatus(survey.Id);
            SetQuestionIdsAndAnswerIds(survey, surveyCreated);
            AddConfigurations(survey);
            AddImages(survey.Questions);
        }

        public void Copy(Survey survey)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("copysurvey");
            var request = new SurveyRequest { Survey = survey };
            var response = _client.Post(uri, request);
            survey.Id = response.Survey.Id;
            SetQuestionIdsAndAnswerIds(survey, response.Survey);
            ResolveAlertConfigurations(survey);
            AddImages(survey.Questions);
        }

        public void Update(Survey survey)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys");
            var request = new SurveyRequest { Survey = survey };
            var response = _client.Put(uri, request);
            SetQuestionIdsAndAnswerIds(survey, response.Survey);
            ResolveAlertConfigurations(survey);
            AddImages(survey.Questions);
        }

        public void Delete(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys/{0}", id);
            _client.Delete<bool>(uri);
        }

        private void ChangeStatus(int id)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            var uri = String.Format("surveys/{0}/Status/{1}", id, (int)SurveyStatus.Approved);
            _client.Put<bool>(uri);
        }

        private void SetQuestionIdsAndAnswerIds(Survey survey, Survey surveyCreated)
        {
            var questionIndex = 0;
            foreach (var question in survey.Questions)
            {
                var answerIndex = 0;
                question.Id = surveyCreated.Questions[questionIndex].Id;
                foreach (var answer in question.Answers)
                {
                    answer.Id = surveyCreated.Questions[questionIndex].Answers[answerIndex].Id;
                    answerIndex++;
                }
                questionIndex++;
            }
        }

        private void SetAlertIds(Survey survey)
        {
            var alertConfigurationResponse = _alertConfigurationService.Filter(new AlertConfigurationFilter { SurveyId = survey.Id });

            foreach (var question in survey.Questions.Where(question => question.Answers.Any()))
            {
                foreach (var answer in question.Answers)
                {
                    var currentAlertConfiguration = alertConfigurationResponse.AlertConfigurations.FirstOrDefault(x => x.AnswerId == answer.Id);
                    answer.AlertId = currentAlertConfiguration.IsNotNull() ? currentAlertConfiguration.AlertId : 0;
                }
            }
        }

        private void AddConfigurations(Survey survey)
        {
            foreach (var question in survey.Questions)
            {
                foreach (var answer in question.Answers.Where(answer => answer.AlertId.IsGreaterThanZero()))
                {
                    _alertConfigurationService.Create(new AlertConfiguration
                    {
                        SurveyId = survey.Id,
                        QuestionId = question.Id,
                        AnswerId = answer.Id,
                        AlertId = answer.AlertId
                    });
                }
            }
        }

        public void ResolveAlertConfigurations(Survey survey)
        {
            var alertConfigurationResponse = _alertConfigurationService.Filter(new AlertConfigurationFilter { SurveyId = survey.Id });

            foreach (var question in survey.Questions)
            {
                foreach (var answer in question.Answers.Where(answer => answer.AlertId.IsGreaterThanZero()))
                {
                    var currentAlertConfiguration = alertConfigurationResponse.AlertConfigurations.FirstOrDefault(x => x.QuestionId.Equals(question.Id) && x.AnswerId.Equals(answer.Id));

                    var request = new AlertConfiguration
                    {
                        Id = currentAlertConfiguration.IsNotNull() ? currentAlertConfiguration.Id : 0,
                        AlertId = answer.AlertId,
                        SurveyId = survey.Id,
                        QuestionId = question.Id,
                        AnswerId = answer.Id
                    };

                    if (request.Id.IsGreaterThanZero())
                        _alertConfigurationService.Update(request);
                    else
                        _alertConfigurationService.Create(request);
                }

                foreach (var alertConfiguration in alertConfigurationResponse.AlertConfigurations)
                {
                    var alertConfigurations = question.Answers.FirstOrDefault(x => question.Id.Equals(alertConfiguration.QuestionId) && x.Id.Equals(alertConfiguration.AnswerId));

                    if (alertConfigurations.IsNull())
                        _alertConfigurationService.Delete(alertConfiguration.Id);
                }
            }


        }

        //todo: make this more ordered
        private void AddImages(IEnumerable<Question> questions)
        {
            _client = new Client(new RestClient { BaseUrl = AppSettings.ServerSurveyApi });
            foreach (var question in questions)
            {
                var hasFilesImages = false;
                if (question.FileImages != null)
                {
                    var uri = String.Format("questions/{0}/image", question.Id);
                    foreach (var fileImage in question.FileImages.Where(fileImage => fileImage.IsNotNull()))
                    {
                        hasFilesImages = true;
                        _client.AddFileByPost<Question>(uri, fileImage);
                    }
                }
                if (!hasFilesImages && question.QuestionImages!=null)
                {
                    var uri = String.Format("questions/{0}/image", question.Id);
                    foreach (var image in question.QuestionImages.Where(image=>image.ImagePath.IsNotNullOrEmpty()))
                    {
                        try
                        {
                            WebClient client = new WebClient();
                            Stream stream = client.OpenRead(image.ImagePath);
                            string fileName = Path.GetFileName(image.ImagePath);
                            _client.AddFileStreamByPost<Question>(uri, stream, fileName);
                            stream.Flush();
                            stream.Close();
                            client.Dispose();
                        }
                        catch (Exception)
                        {
                        }
                    }

                }

            }
        }
    }
}