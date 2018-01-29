namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class User
    {
        public User()
        {
            Id = 0;
            Name = "";
            BranchId = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
    }
}