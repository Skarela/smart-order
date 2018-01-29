using System;
using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Util {
   public static class BootstrapWbcHelper {

      #region Methods

      public static IHtmlString StatusBar(this HtmlHelper helper, float percentDone, float percentFail,
                                            float percentProgress, int done, int total) {
                                               var progressBar = GetProgressBar(percentDone, percentFail, percentProgress, done, total);
         return new HtmlString(progressBar);
      }

      //------------------------------------------------------------

      public static IHtmlString DateTimeFormated(this HtmlHelper helper, string dateTime) {
         var formatedDateTime = FormatDateTime(dateTime);
         return new HtmlString(formatedDateTime);
      }

      //------------------------------------------------------------

      public static string StatusBar(float percentDone, float percentFail, float percentProgress, int done, int total) {
         var progressBar = GetProgressBar(percentDone, percentFail, percentProgress, done, total );
         return progressBar;
      }

      //------------------------------------------------------------

      public static string DateTimeFormated(string dateTime) {
         var formatedDateTime = FormatDateTime(dateTime);
         return formatedDateTime;
      }

      //------------------------------------------------------------

      #endregion


      #region Private Rutines

      private static string GetProgressBar(float percentDone, float percentFail, float percentProgress, int done, int total) {
         var progressBar =
            @"    <div class='progress' style='margin-bottom:0px; height: 15px !important;'>
                                    <div class='bar bar-success' style='width: " + percentDone + @"%'></div>
                                    <div class='bar bar-warning' style='width: " + percentProgress + @"%'></div>
                                    <div class='bar bar-danger' style='width: " + percentFail + @"%'></div>
                                </div>
                                <div>
                                    <small style='color:#999;'>" + percentDone.ToString("F") + @"% Completado (" + done + " de " + total + @")</small>
                                </div>
                              ";
         return progressBar;
      }

      //------------------------------------------------------------

      private static string FormatDateTime(string dateTime) {
         if (dateTime == null) {
            return "";
         }
         var date = DateTime.Parse(dateTime);
         var formatDate = @"
                                 <div >" + date.ToShortDateString() + @"</div>
                                 <div ><small style='color:#999'>" +
                          date.ToShortTimeString() + "</small></div>";
         return formatDate;
      }

      //------------------------------------------------------------

      #endregion
   }
}