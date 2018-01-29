using System.Web.Mvc;
using System.Web.Routing;
using siteSmartOrder.Infrastructure.SimpleInjector;

namespace siteSmartOrder
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            SimpleInjectorConfig.Register();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "siteSmartOrder.Controllers" }
            );

        }
    }
}