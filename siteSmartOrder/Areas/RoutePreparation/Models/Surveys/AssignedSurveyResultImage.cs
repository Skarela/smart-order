using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class AssignedSurveyResultImage : File
    {
        public AssignedSurveyResultImage()
        {
            Id = 0;
            ImagePath = "";
        }

        public int Id { get; set; }
        public string ImagePath
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }
    }
}