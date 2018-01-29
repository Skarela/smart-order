using System.Web;
using System.Web.Mvc;

namespace siteSmartOrder.Infrastructure.Helpers
{
    public static class MapHelper
    {
        public static IHtmlString UrlGoogleMaps(this HtmlHelper helper)
        {
            const string key = "AIzaSyA71xSXo0WqAggKzGYSECHAIcOC3VXlONI";
            var htmlString = string.Format("http://maps.googleapis.com/maps/api/js?key={0}&signed_in=false", key);
            return new HtmlString(htmlString);
        }
    }
}