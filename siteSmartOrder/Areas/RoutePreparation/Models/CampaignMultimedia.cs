using siteSmartOrder.Areas.RoutePreparation.Models.Files;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class CampaignMultimedia : File
    {
        public const string Created = "created";
        public const string Added = "added";
        public const string Removed = "removed";
        public const string Unchanged = "unchanged";
        public const string Ignore = "ignore";//substitute removed with this status when creating a new campaign

        public CampaignMultimedia()
        {
            Id = 0;
            Path = "";
            MultimediaType = 0;
            CampaignId = 0;
            Group = "";
        }

        public int Id { get; set; }

        public string Path
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }

        public int MultimediaType { get; set; }
        public int CampaignId { get; set; }
        public string Group { get; set; }
        public string State { get; set; }

        public string FileUploadedName { get; set; }

        public string Name 
        {
            get 
            {
                return GetName();
            }
        }

        private string GetName()
        {
            if(string.IsNullOrWhiteSpace(Path)) return null;
            var parts = Path.Split('/');
            return parts[parts.Length - 1];
        }
    }
}