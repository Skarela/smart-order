using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Infrastructure.Helpers
{
    public static class TableHelper
    {
        public static IHtmlString BasicTable(this HtmlHelper helper, bool withDeleteManyButton)
        {
            return BasicTable(helper, withDeleteManyButton, "Table");
        }

        public static IHtmlString BasicTable(this HtmlHelper helper, bool withDeleteManyButton, string tableId)
        {
            return BasicTable(helper, withDeleteManyButton, tableId, "");
        }

        public static IHtmlString BasicTable(this HtmlHelper helper, bool withDeleteManyButton, string tableId, string otherButton)
        {
            var deleteManyButton = "<div id='" + tableId + "Toolbar' hidden='hidden'>" +
                                   otherButton +
                                   "<button type='button' id='" + tableId +
                                   "DeleteMany' class='btn btn-sm btn-danger pull-right' disabled>" +
                                   "<i class='fa fa-trash-o'></i> Borrar Seleccionados" +
                                   "</button>" +
                                   "</div>";

            const string component = "<div class='padding-bottom-20 clear-both'>" +
                                     "<div id='{1}Filter' class='table-filter'></div>" +
                                     "{0}" +
                                     "<table id='{1}'></table>" +
                                     "</div>" +
                                     "<div id='{1}Loading' class='col-lg-12 table-loadding'>" +
                                     "<i class='fa fa-cog fa-spin'></i><span id='{1}LoadingText'> Generando tabla... </span>" +
                                     "</div>";
            var htmlString = string.Format(component, withDeleteManyButton ? deleteManyButton : "", tableId);
            return new HtmlString(htmlString);
        }
    }
}