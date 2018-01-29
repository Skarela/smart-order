namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class CoolerConfigurationReplyFilter : Filter
    {
        public CoolerConfigurationReplyFilter()
        {
            CoolerConfigurationId = 0;
            BranchId = 0;
            UserId = 0;
            CoolerId = "";
            CustomerId = 0;
            UserIds = "";
            CustomerIds = "";
            FromCreationDate = "";
            ToCreationDate = "";
        }

        public int CoolerConfigurationId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string CoolerId { get; set; }
        public int CustomerId { get; set; }
        public string UserIds { get; set; }
        public string CustomerIds { get; set; }
        public string FromCreationDate { get; set; }
        public string ToCreationDate { get; set; }
    }
}