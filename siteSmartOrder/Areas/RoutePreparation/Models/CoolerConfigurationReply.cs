namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    public class CoolerConfigurationReply
    {
        public CoolerConfigurationReply()
        {
            Id = 0;
            CoolerConfigurationId = 0;
            ApplyAssignedSurveyId = 0;
            UserId = 0;
            CoolerId = "";
            Exists = true;
            GoodCondition = true;
            Contaminated = true;
            NewCoolerId = 0;
            CreationDate = "";
            CustomerId = 0;
        }

        public int Id { get; set; }
        public int CoolerConfigurationId { get; set; }
        public int ApplyAssignedSurveyId { get; set; }
        public int UserId { get; set; }
        public string CoolerId { get; set; }
        public bool Exists { get; set; }
        public bool GoodCondition { get; set; }
        public bool Contaminated { get; set; }
        public int NewCoolerId { get; set; }
        public string CreationDate { get; set; }
        public int CustomerId { get; set; }
    }
}