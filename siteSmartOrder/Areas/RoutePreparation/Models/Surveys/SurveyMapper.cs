using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Requests;
using System.Web.Script.Serialization;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys.Mappers
{
    public class SurveyMapper
    {
        public Survey Clone(Survey survey)
        {

            var jss = new JavaScriptSerializer();
            var serialSurvey=jss.Serialize(survey);
            var surveyCopy = jss.Deserialize<Survey>(serialSurvey);
            var originalQuestions=survey.Questions.ToArray();
            var copyQuestions = surveyCopy.Questions.ToArray();
            for (int i = 0; i < copyQuestions.Length; i++)
            {
                if ( originalQuestions[i].FileImages != null)
                {
                    copyQuestions[i].FileImages = originalQuestions[i].FileImages;
                    //copyQuestions[i].FileImages.ForEach(f => f.InputStream.Position = 0);
                    foreach (var image in copyQuestions[i].FileImages)
                    {
                        if (image != null)
                        {
                            image.InputStream.Position = 0L;
                        }
                    }
                }
            }
            return surveyCopy;
        }

    }
}
