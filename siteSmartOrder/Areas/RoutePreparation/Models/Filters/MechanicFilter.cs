namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class MechanicFilter : Filter
    {
        public MechanicFilter()
        {
            Name = "";
            BranchId = 0;
            Code = "";
        }

        public string Name { get; set; }
        public int BranchId { get; set; }
        public string Code { get; set; }
    }
}