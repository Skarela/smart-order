using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Infrastructure.Helpers
{
    public static class AlertHelper
    {
        private const string Template = @"<div class='alert {0} alert-dismissable' style='clear:both;'>
                                        <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>
                                        <i class='fa {1}'></i> {2}
                                        </div>";

        public static IHtmlString Success(this HtmlHelper helper, string message)
        {
            const string alertClass = "alert-success";
            const string alertIcon = "fa-check-circle";
            var htmlString = string.Format(Template, alertClass, alertIcon, message);
            return new HtmlString(htmlString);
        }

        public static IHtmlString Warning(this HtmlHelper helper, string message)
        {
            const string alertClass = "alert-warning";
            const string alertIcon = "fa-exclamation-triangle";
            var htmlString = string.Format(Template, alertClass, alertIcon, message);
            return new HtmlString(htmlString);
        }

        public static IHtmlString Failure(this HtmlHelper helper, string message)
        {
            const string alertClass = "alert-danger";
            const string alertIcon = "fa-times-circle";
            var htmlString = string.Format(Template, alertClass, alertIcon, message);
            return new HtmlString(htmlString);
        }

        public static IHtmlString Information(this HtmlHelper helper, string message)
        {
            const string alertClass = "alert-info";
            const string alertIcon = "fa-info-circle";
            var htmlString = string.Format(Template, alertClass, alertIcon, message);
            return new HtmlString(htmlString);
        }
    }
}