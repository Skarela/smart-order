namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class SosAlert
    {
        public SosAlert()
        {
            Id = 0;
            UserId = 0;
            IncidentId = 0;
            RouteId = 0;
            Comment = "";
            Latitude = 0;
            Longitude = 0;
            Status = "";
            UpdatedAt = "";
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int IncidentId { get; set; }
        public int RouteId { get; set; }
        public string Comment { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Status { get; set; }
        public string UpdatedAt { get; set; }
    }
}