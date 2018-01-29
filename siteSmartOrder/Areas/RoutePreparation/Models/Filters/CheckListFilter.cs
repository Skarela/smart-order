namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class ChecklistFilter : Filter
    {
        public ChecklistFilter()
        {
            BranchId = 0;
            UserPortalId = 0;
        }

        public int BranchId { get; set; }
        public int UserPortalId { get; set; }
        public bool IsTemplate { get; set; }
    }
}