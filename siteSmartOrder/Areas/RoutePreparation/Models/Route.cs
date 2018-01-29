namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Route
    {
        public Route()
        {
            Id = 0;
            Name = "";
            BranchId = 0;
            Code = "";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
    }
}