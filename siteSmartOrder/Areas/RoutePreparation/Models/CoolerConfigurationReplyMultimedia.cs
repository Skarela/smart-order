using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class CoolerConfigurationReplyMultimedia : File
    {
        public CoolerConfigurationReplyMultimedia()
        {
            Id = 0;
            Path = "";
            CoolerConfigurationReplyId = 0;
        }

        public int Id { get; set; }

        public string Path
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }

        public int CoolerConfigurationReplyId { get; set; }
    }
}