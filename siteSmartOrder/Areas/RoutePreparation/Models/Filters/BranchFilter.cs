namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class BranchFilter : Filter
    {
        public BranchFilter()
        {
            Code = "";
            Name = "";
            BranchId = null;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public int? BranchId { get; set; }
    }
}