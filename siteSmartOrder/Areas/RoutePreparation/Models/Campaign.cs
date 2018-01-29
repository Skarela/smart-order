using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using siteSmartOrder.Infrastructure.Extensions;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Campaign
    {
        private readonly List<HttpPostedFileBase> _files;

        public Campaign()
        {
            Id = 0;
            Name = "";
            Description = "";
            StartDate = "";
            EndDate = "";
            SurveyId = 0;
            BranchId = 0;
            UserPortalId = 0;
            CampaignMultimediaType = 0;
            CampaignMultimedias = new List<CampaignMultimedia>();
            _files = new List<HttpPostedFileBase>();
            IsTemplate = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int SurveyId { get; set; }
        public int BranchId { get; set; }
        public int UserPortalId { get; set; }
        public int CampaignMultimediaType { get; set; }
        public List<CampaignMultimedia> CampaignMultimedias { get; set; }
        public bool IsTemplate { get; set; }
        [ScriptIgnore]
        public List<HttpPostedFileBase> Files
        {
            get { return _files; }
            set
            {
                var files = value;
                foreach (var file in files.Take(3).Where(file => file.IsNotNull() && file.ContentLength.IsGreaterThanZero()))
                {
                    _files.Add(file);
                }
            }
        }
    }
}