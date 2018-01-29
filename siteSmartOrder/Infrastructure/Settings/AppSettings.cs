using System;
using System.Configuration;
using System.Web;

namespace siteSmartOrder.Infrastructure.Settings
{
    public static class AppSettings
    {
        public static Uri ServerSurveyApi
        {
            get { return new Uri(ConfigurationManager.AppSettings["SurveyApiServer"]); }
        }

        public static Uri ServerSurveyEngineApi
        {
            get { return new Uri(ConfigurationManager.AppSettings["SurveyEngineApiServer"]); }
        }

        public static Uri ServerSmartOrderApi
        {
            get { return new Uri(ConfigurationManager.AppSettings["PortalServer"]); }
        }

        public static Uri ServerIncidentApi
        {
            get { return new Uri(ConfigurationManager.AppSettings["IncidentApiServer"]); }
        }

        public static string FilesFolder
        {
            get
            {
                var request = HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}{3}", request.Url.Scheme, request.Url.Authority, request.ApplicationPath, "/Areas/RoutePreparation/Content/Images/Files/");
            }
        }

        public static string PointersFolder
        {
            get
            {
                var request = HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}{3}", request.Url.Scheme, request.Url.Authority, request.ApplicationPath, "/Areas/RoutePreparation/Content/Images/Pointers/");
            }
        }

        public static string IconsFolder
        {
            get
            {
                var request = HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}{3}", request.Url.Scheme, request.Url.Authority, request.ApplicationPath, "/Areas/RoutePreparation/Content/Images/Icons/");
            }
        }
    }
}
