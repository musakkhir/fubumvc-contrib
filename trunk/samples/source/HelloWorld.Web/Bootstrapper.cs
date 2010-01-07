using System.Collections.Generic;
using System.Web.Routing;
using FubuMVC.Core.Runtime;
using FubuMVC.StructureMap;
using StructureMap;

namespace HelloWorld.Web
{
    public class Bootstrapper : IBootstrapper
    {
        private readonly ICollection<RouteBase> _routes;

        private Bootstrapper(ICollection<RouteBase> routes)
        {
            _routes = routes;
        }

        public void BootstrapStructureMap()
        {
            UrlContext.Reset();

            BootstrapFubu(ObjectFactory.Container, _routes);
        }

        public static void BootstrapFubu(IContainer container, ICollection<RouteBase> routes)
        {
            var bootstrapper = new StructureMapBootstrapper(container, new HelloWorldFubuRegistry());
            bootstrapper.Bootstrap(routes);
        }

        public static void Bootstrap(RouteCollection routes)
        {
            new Bootstrapper(routes).BootstrapStructureMap();
        }
    }
}