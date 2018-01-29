namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class UserFilter : Filter
    {
        public UserFilter()
        {
            Name = "";
            BranchId = 0;
        }

        public string Name { get; set; }
        public int BranchId { get; set; }
    }
}