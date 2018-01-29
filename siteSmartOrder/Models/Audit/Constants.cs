namespace siteSmartOrder.Models.Audit
{
   public class Constants
   {
      /*==================================================================*/
      /*                            C O L U M N S                         */
      /*==================================================================*/

       //StatusColum--------------------------------------------

       public static readonly string StatusUnStartedColumn = "<span class='label label-info' style='font-size: 11px;'><center>Sin comenzar</center></span>";
       public static readonly string StatusInProgressColumn = "<span class='label label-warning' style='font-size: 11px;'><center>En progreso</center></span>";
       public static readonly string StatusFinalizedColumn = "<span class='label label-success' style='font-size: 11px;'><center>Finalizado</center></span>";

       //DetailColumn--------------------------------------------
      public static readonly string DetailColumn = ButtonDefault(IconEyed);

      //ExtendColum--------------------------------------------
      public static readonly string ExtendColumn = ButtonDefault(IconTime);
      public static readonly string ExtendColumnDisabled = ButtonDefaultDisabled(IconTime);

      //Style Used-----------------------------------------------
      public static readonly string UsedStart = Used("En uso");
      public static readonly string VisitUsedStart = Used("Visitas");
      public const string UsedEnd = "</small></center>";

      //SuccessColumn------------------------------------------------
      public static readonly string SuccessStart = ButtonSuccessStart();
      public static readonly string SuccessEnd = ButtonSuccessEnd();

      //SuccessColumn------------------------------------------------
      public static readonly string CountStartInfo = ButonCountStart("btn-info");
      public static readonly string CountStartDanger = ButonCountStart("btn-danger");
      public static readonly string CountStartSuccess = ButonCountStart("btn-success");
      public static readonly string CountEnd = ButonCountEnd();

      /*==================================================================*/
      /*                             I C O N S                            */
      /*==================================================================*/
      private const string IconEyed = "icon-eye-open";
      private const string IconTime = "icon-time";
      public const string IconTrue = "btn-success";
      public const string IconFalse = "btn-danger";

      /*==================================================================*/
      /*                   P R I V A T E    R U T I N E S                 */
      /*==================================================================*/

      private static string ButtonDefault(string icon)
      {
         return "<center><a style='cursor: pointer'><i class='" + icon + "'></i></a></center>";
      }

      private static string ButtonDefaultDisabled(string icon)
      {
          return "<center><a  style='cursor: no-drop' disabled><i class='" + icon + "' style='opacity: .5;'></i></a></center>";
      }

      private static string Used(string text)
      {
         return "<center><small style='color:#4fa950'>" + text + "</small></center><center><small style='color:#999'>";
      }

      private static string ButtonSuccessStart()
      {
         return "<center><a class='";
      }

      private static string ButtonSuccessEnd()
      {
         return " ui-link' style='padding: 1px 9px; line-height: 35px; border-radius: 13px;'></a></center>";
      }

      private static string ButonCountStart(string color)
      {
         return
            "<center><a class='" + color + " ui-link' style='padding: 1px 10px; line-height: 35px; border-radius: 15px;'><b> ";
      }

      private static string ButonCountEnd() {
         return " </b></a></center>";
      }
   }
}