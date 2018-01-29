using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class WorkshopReplyMultimedia : File
    {
        public WorkshopReplyMultimedia()
        {
            Id = 0;
            Path = "";
            WorkshopReplyId = 0;
        }

        public int Id { get; set; }

        public string Path
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }

        public int WorkshopReplyId { get; set; }
    }
}