using System.Collections;
using System.Web.Routing;
using FubuMVC.Core;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Conventions;
using Microsoft.Practices.ServiceLocation;
using FubuMVC.Container.StructureMap.Config;
using StructureMap;

namespace AltOxite.Web
{
    public class Bootstrapper : IBootstrapper
    {
        private static bool _hasStarted;

        public virtual void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new FrameworkServicesRegistry());
                x.AddRegistry(new AltOxiteWebRegistry());
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
            if (_hasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                Bootstrap();
                _hasStarted = true;
            }

        }

        public static void Bootstrap()
        {
            
            new Bootstrapper().BootstrapStructureMap();
        }
    }
}
