using System;
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
        public void creates_a_new_principal_with_the_id_as_a_guid()
        {
            var id = Guid.NewGuid();
            var identity = new GenericIdentity(id.ToString());
            new LoggedOnPrincipleFactory<Guid>()
                .CreatePrincipal(identity)
                .ShouldBeOfType<LoggedOnPrincipal<Guid>>()
                .UserId.ShouldEqual(id);
        }
    }
}