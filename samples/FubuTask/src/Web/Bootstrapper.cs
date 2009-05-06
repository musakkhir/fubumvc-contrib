using System.Web.Routing;
using FubuMVC.Core.Controller.Config;
using FubuTask.Config;
using Microsoft.Practices.ServiceLocation;
using FubuMVC.Container.StructureMap.Config;
using StructureMap;

namespace FubuTask
{
    public class Bootstrapper : IBootstrapper
    {
        public static bool HasStarted{ get; private set; }

        public virtual void BootstrapStructureMap()
        {
            MvcConfiguration.Configure();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new FrameworkServicesRegistry());
                x.AddRegistry(new WebRegistry());
                x.AddRegistry(new PersistenceRegistry());
                x.AddRegistry(new ControllerConfig());
            });

            ObjectFactory.AssertConfigurationIsValid();

            setup_service_locator();

            initialize_routes();
        }

        private static void setup_service_locator()
        {
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
        }

        private static void initialize_routes()
        {
            ObjectFactory.GetInstance<IRouteConfigurer>().LoadRoutes(RouteTable.Routes);
        }

        public static void Restart()
        {
            if (HasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                Bootstrap();
                HasStarted = true;
            }

        }

        public static void Bootstrap()
        {

            new Bootstrapper().BootstrapStructureMap();
        }
    }
}