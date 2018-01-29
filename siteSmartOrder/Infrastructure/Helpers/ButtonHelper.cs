using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Infrastructure.Helpers
{
    public static class ButtonHelper
    {
        public static IHtmlString ButtonExport(this HtmlHelper helper)
        {
            const string exportButton = "<a id='btnExport' class='btn btn-default btn-sm iconPointer margin-bottom-5' style='float:right;'><i class='fa fa-file-excel-o'></i> Exportar a Excel</a>";
            return new HtmlString(exportButton);
        }
        public static IHtmlString ButtonExportWithReport(this HtmlHelper helper)
        {
            const string exportButtons = @"<div class='btn-group'style='float:right;'>
                    <a class='btn dropdown-toggle' data-toggle='dropdown' href='#'>
                        <i class='fa fa-file-excel-o'></i> Exportar a Excel
                        <span class='caret'></span>
                    </a>
                    <ul class='dropdown-menu'>
                        <li> <a id='btnExport' class='iconPointer'>Lista de resultados</a></li>
                        <li> <a id='btnExportReport' class='iconPointer'>Reporte de resultados</a></li>
                    </ul>
                </div>";
            return new HtmlString(exportButtons);
        }

        public static IHtmlString ButtonsForm(this HtmlHelper helper, string uriCancelButton, bool isNew)
        {
            var labelButton = isNew ? "Crear" : "Editar";

            const string stringToButtons = @"
                                                    <div class='btn-success btn-group pull-left dropdown margin-bottom-15'>
                                                        <button id='Create' type='submit' class='btn btn-success'>{1}</button>
                                                        <button type='button' class='btn btn-success dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>
                                                        <span class='caret'></span>
                                                        </button>
                                                        <ul class='dropdown-menu left'>
                                                        <li> <a id='CreateAndNew' class='iconPointer'value='New'>{1} y nuevo</a></li>
                                                        </ul>
                                                    </div>
                                                   <input type='hidden' id='View' name='View' value='' />
                                                   <a class='btn btn-default margin-left-10' href='{0}'>Cancelar</a>";
            var htmlString = string.Format(stringToButtons, uriCancelButton, labelButton);
            return new HtmlString(htmlString);
        }

    }
}