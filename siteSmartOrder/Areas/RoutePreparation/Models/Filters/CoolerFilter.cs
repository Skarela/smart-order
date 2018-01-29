namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CoolerFilter : Filter
    {
        public CoolerFilter()
        {
            CustomerId = 0;
            Serie = "";
        }

        public int CustomerId { get; set; }
        public string Serie { get; set; }
    }
}