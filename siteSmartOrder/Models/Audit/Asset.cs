namespace siteSmartOrder.Models.Audit
{
    public class Asset
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Observation { get; set; }
        public bool Synchronized { get; set; }
   }
}