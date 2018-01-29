using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Multimedia : File
    {
        public Multimedia()
        {
            Id = 0;
            Path = "";
            AlertId = 0;
        }

        public int Id { get; set; }

        public string Path
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }
        public string UrlFile
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }

        public EMultimediaType MultimediaType
        {
            get;
            set;
        }
        public int AlertId { get; set; }
    }

    public enum EMultimediaType
    {
        Image = 1,
        Audio = 2
    }
}