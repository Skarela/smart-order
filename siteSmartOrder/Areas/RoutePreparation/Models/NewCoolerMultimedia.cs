using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class NewCoolerMultimedia : File
    {
        public NewCoolerMultimedia()
        {
            Id = 0;
            Path = "";
            NewCoolerId = 0;
        }

        public int Id { get; set; }

        public string Path
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }

        public int NewCoolerId { get; set; }
    }
}