namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Mechanic
    {
        public Mechanic()
        {
            Id = 0;
            Code = "";
            Name = "";
            BranchId = 0;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
    }
}