namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class WorkshopReplyFilter : Filter
    {
        public WorkshopReplyFilter()
        {
            BranchId = 0;
            MechanicId = 0;
            CreationDate = "";
            UnitId = 0;
            UnitIds = "";
            MechanicIds = "";
            FromCreationDate = "";
            ToCreationDate = "";
        }

        public int BranchId { get; set; }
        public int MechanicId { get; set; }
        public string CreationDate { get; set; }
        public int UnitId { get; set; }
        public string UnitIds { get; set; }
        public string RouteIds { get; set; }
        public string MechanicIds { get; set; }
        public string FromCreationDate { get; set; }
        public string ToCreationDate { get; set; }
    }
}