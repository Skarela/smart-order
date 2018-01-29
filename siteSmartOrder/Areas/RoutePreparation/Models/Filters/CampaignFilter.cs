namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CampaignFilter : Filter
    {
        public CampaignFilter()
        {
            Name = "";
            OnlyToday = true;
            BranchId = 0;
            UserPortalId = 0;
            StartDate = "";
            EndDate = "";
        }

        public string Name { get; set; }
        public bool OnlyToday { get; set; }
        public int BranchId { get; set; }
        public int UserPortalId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsTemplate { get; set; }
    }
}