namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CampaignReplyFilter : Filter
    {
        public CampaignReplyFilter()
        {
            BranchId = 0;
            UserId = 0;
            CampaignId = 0;
            CampaignIds = "";
            UserIds = "";
            CreationDate = "";
            RouteId = 0;
            RouteIds = "";
            FromCreationDate = "";
            ToCreationDate = "";
        }

        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int CampaignId { get; set; }
        public string CampaignIds { get; set; }
        public string UserIds { get; set; }
        public string CreationDate { get; set; }
        public int RouteId { get; set; }
        public string RouteIds { get; set; }
        public string FromCreationDate { get; set; }
        public string ToCreationDate { get; set; }
    }
}