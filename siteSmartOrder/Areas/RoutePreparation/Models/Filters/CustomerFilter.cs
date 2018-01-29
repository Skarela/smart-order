namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CustomerFilter : Filter
    {
        public CustomerFilter()
        {
            BranchId = 0;
            CoolerId = "";
            UserId = 0;
            Code = "";
            Name = "";
        }

        public int BranchId { get; set; }
        public string CoolerId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}