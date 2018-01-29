using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace siteSmartOrder.Infrastructure.SimpleInjector
{
    public static class SimpleInjectorConfig
    {
        public static void Register()
        {
            var container = new Container();
            SimpleInjectorModule.SetContainer(container);
            SimpleInjectorModule.Load();
            SimpleInjectorModule.VerifyContainer();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}