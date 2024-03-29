using System;
using System.Security.Principal;
using Fohjin.Core;
using Fohjin.Core.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace Fohjin.Tests.Security
{
    [TestFixture]
    public class FohjinPrincipalFactoryTester
    {
        [Test]
        public void creates_a_new_principal_with_the_ID_as_a_guid()
        {
            var identity = MockRepository.GenerateStub<IIdentity>();
            var userId = Guid.NewGuid();
            identity.Stub(i => i.Name).Return(userId.ToString());

            new FohjinPrincipalFactory()
                .CreatePrincipal(identity)
                .ShouldBeOfType<FohjinPrincipal>()
                .UserId.ShouldEqual(userId);
        }
    }
}