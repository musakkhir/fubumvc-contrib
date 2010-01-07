using System;

namespace HelloWorld.Web.Controllers.Hello
{
    public class HelloController
    {
        public HelloViewModel Hello()
        {
            return new HelloViewModel{ Text = "Hello World" };
        }

        public HelloWorldJsonViewModel HelloJson()
        {
            return new HelloWorldJsonViewModel { Id = Guid.NewGuid(), Name = "JSON Test" };
        }
    }
}