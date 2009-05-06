using System;
using System.Web;

namespace FubuTask
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Bootstrap();
        }
    }
}