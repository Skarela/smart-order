namespace siteSmartOrder.Models.Audit
{
    public class UserToAuditCampaign
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string StatusColumn { set; get; }

        public string AuditsColumn
        {
            get { return Constants.DetailColumn; }
        }

   }
}