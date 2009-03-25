using System;
using FubuMVC.Container.StructureMap.Config;
using FubuMVC.Core.Behaviors;
using FubuSample.Core.Web;

namespace FubuSample.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ControllerConfig.Configure = x =>
             {
                 x.ByDefault.EveryControllerAction(d => d
                     .Will<execute_the_result>());

                 x.AddControllersFromAssembly.ContainingType<ViewModel>(c =>
                    {
                        c.Where(t => t.Namespace.EndsWith("Web.Controllers")
                                     && t.Name.EndsWith("Controller"));

                        c.MapActionsWhere((m, i, o) => true);
                    });
             };

            Bootstrapper.Bootstrap();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}