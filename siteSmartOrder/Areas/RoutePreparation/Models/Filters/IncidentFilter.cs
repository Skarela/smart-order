namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class IncidentFilter : Filter
    {
        public IncidentFilter()
        {
            OnlyAvailables = false;
        }

        public bool OnlyAvailables { get; set; }
    }
}