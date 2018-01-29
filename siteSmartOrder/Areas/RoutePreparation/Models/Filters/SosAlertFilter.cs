namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class SosAlertFilter : Filter
    {
        public SosAlertFilter()
        {
            StartDate = "";
            IncidentId = null;
            Status = "";
            UserId = null;
            RouteId = null;
            BranchId = null;
        }

        public string StartDate { get; set; }
        public int? IncidentId { get; set; }
        public string Status { get; set; }
        public int? UserId { get; set; }
        public int? RouteId { get; set; }
        public int? BranchId { get; set; }
    }
}