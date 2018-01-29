namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class ChecklistReplyFilter : Filter
    {
        public ChecklistReplyFilter()
        {
            BranchId = 0;
            UserId = 0;
            CreationDate = "";
            RouteIds = "";
            UserIds = "";
            FromCreationDate = "";
            ToCreationDate = "";
        }

        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string CreationDate { get; set; }
        public string RouteIds { get; set; }
        public string UserIds { get; set; }
        public string FromCreationDate { get; set; }
        public string ToCreationDate { get; set; }
    }
}