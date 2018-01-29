using siteSmartOrder.Areas.RoutePreparation.Enums;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Surveys
{
    public class Category
    {
        public Category()
        {
            Id = (int)CategoryType.None;
            Name = "";
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}