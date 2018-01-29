namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Incidence
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string User { get; set; }
        public string Branch { get; set; }
        public string RouteId { get; set; }
        public string UnitCode { get; set; }
        public string CreatedOn { get; set; }
    }
}