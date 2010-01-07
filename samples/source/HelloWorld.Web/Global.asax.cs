using System;
using System.Web;
using System.Web.Routing;

namespace HelloWorld.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var routeCollection = RouteTable.Routes;
            Bootstrapper.Bootstrap(routeCollection);
        }
    }
}