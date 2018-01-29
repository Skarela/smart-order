namespace siteSmartOrder.Models
{
    public class Branch
    {
        public int branchId { set; get; }
        public string code { set; get; }
        public string name { set; get; }
        public int? companyId { set; get; }
        public int ClosureTypeId { get; set; }
    }
}