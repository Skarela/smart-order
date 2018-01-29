namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class ManagerFilter : Filter
    {
        public ManagerFilter()
        {
            IncidentId = null;
            Name = "";
            Company = "";
            Email = "";
            BranchId = null;
        }

        public int? IncidentId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public int? BranchId { get; set; }
    }
}