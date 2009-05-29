using System.Security.Principal;
using FubuMVC.Framework.Security;
using FubuMVC.Tests;
using NUnit.Framework;

namespace FubuMVC.Framework.Tests.Security
{
    [TestFixture]
    public class LoggedOnPrincipleFactoryTester
    {
        [Test]
        public void creates_a_new_principal()
        {
            var username = "bob";
            var identity = new GenericIdentity(username);
            new LoggedOnPrincipleFactory()
                .CreatePrincipal(identity)
                .ShouldBeOfType<LoggedOnPrincipal>()
                .Username.ShouldEqual(username);
        }
    }
}