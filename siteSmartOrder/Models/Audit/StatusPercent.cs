namespace siteSmartOrder.Models.Audit
{
   public class StatusPercent
   {
      public float PercentDone { get; set; }
      public float PercentFail { get; set; }
      public float PercentProgress { get; set; }
      public int Done { get; set; }
      public int Total { get; set; }

      public StatusPercent(float percentDone, float percentFail, float percentProgress, int done, int total)
      {
         PercentDone = percentDone;
         PercentFail = percentFail;
         PercentProgress = percentProgress;
         Done = done;
         Total = total;
      }

      public StatusPercent()
      {
         PercentDone = 0;
         PercentFail = 0;
         PercentProgress = 0;
         Done = 0;
         Total = 0;
      }
   }
}