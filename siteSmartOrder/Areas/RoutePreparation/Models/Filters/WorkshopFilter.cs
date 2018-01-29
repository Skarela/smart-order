namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class    WorkshopFilter: Filter
    {
        public WorkshopFilter()
        {
            BranchId = 0;
            UserPortalId = 0;
        }

        public int BranchId { get; set; }
        public int UserPortalId { get; set; }
    }
}