namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class ContactFilter : Filter
    {
        public ContactFilter()
        {
            Name = "";
            BranchId = 0;
            WithoutBranch = false;
        }

        public string Name { get; set; }
        public int BranchId { get; set; }
        public bool WithoutBranch { get; set; }
    }
}