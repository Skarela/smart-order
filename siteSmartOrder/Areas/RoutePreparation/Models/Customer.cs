namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Customer
    {
        public Customer()
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