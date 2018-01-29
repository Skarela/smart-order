namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class RouteFilter : Filter
    {
        public RouteFilter()
        {
            Code = "";
            Name = "";
            Code = "";
        }

        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}