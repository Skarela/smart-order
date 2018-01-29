namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class SosAlertComment
    {
        public SosAlertComment()
        {
            Id = 0;
            Comment = "";
        }

        public int Id { get; set; }
        public string Comment { get; set; }
    }
}