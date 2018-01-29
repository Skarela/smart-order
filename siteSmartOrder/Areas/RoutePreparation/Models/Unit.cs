namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Unit
    {
        public Unit()
        {
            Id = 0;
            Code = "";
            RouteId = 0;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int RouteId { get; set; }
    }
}