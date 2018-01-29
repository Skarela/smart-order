namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Branch
    {
        public Branch()
        {
            Id = 0;
            Name = "";
            Code = "";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}