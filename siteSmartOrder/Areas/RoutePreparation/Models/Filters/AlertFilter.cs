namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class AlertFilter : Filter
    {
        public AlertFilter()
        {
            Name = "";
            Type = 0;
            ContactId = 0;
        }

        public string Name { get; set; }
        public int Type { get; set; }
        public int ContactId { get; set; }
    }
}