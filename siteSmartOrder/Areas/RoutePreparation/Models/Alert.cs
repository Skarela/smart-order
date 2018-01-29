using siteSmartOrder.Areas.RoutePreparation.Resolvers;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class Alert
    {
        public Alert()
        {
            Id = 0;
            Name = "";
            Description = "";
            Type = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string DisplayType {
            get { return Type.ResolverAlertType(); }
        }
    }
}