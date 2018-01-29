using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class QuestionImage : File
    {
        public QuestionImage()
        {
            Id = 0;
        }

        public int Id { get; set; }

        public string ImagePath
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }
    }
}