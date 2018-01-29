namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Cooler
    {
        public Cooler()
        {
            Id = "";
            Name = "";
            Serie = "";
            DoorsNumber = 0;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Serie { get; set; }
        public int DoorsNumber { get; set; }
    }
}