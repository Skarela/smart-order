using System.Web.Mvc;

namespace siteSmartOrder.Areas.RoutePreparation
{
    public class RoutePreparationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "RoutePreparation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "RoutePreparation_default",
                "RoutePreparation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
