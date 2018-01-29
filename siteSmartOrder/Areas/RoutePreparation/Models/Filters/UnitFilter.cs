namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class UnitFilter : Filter
    {
        public UnitFilter()
        {
            Code = null;
            RoutetId = 0;
        }

        public string Code { get; set; }
        public int RoutetId { get; set; }
    }
}