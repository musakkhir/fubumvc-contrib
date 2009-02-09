using System;
using System.Security.Principal;
using System.Threading;
using Fohjin.Core;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Web.Behaviors;
using NUnit.Framework;
using Rhino.Mocks;

namespace Fohjin.Tests.Web.Behaviors
{
    [TestFixture]
    public class SetUpCurrentUserTester
    {
        private set_the_current_logged_in_user_on_the_output_viewmodel _behavior;
        private IRepository _repo;
        private IPrincipal _previousPrin;
        private IIdentity _identity;
        private Guid _userId;

        [SetUp]
        public void SetUp()
        {
            _previousPrin = Thread.CurrentPrincipal;
            _identity = MockRepository.GenerateStub<IIdentity>();
            _repo = MockRepository.GenerateMock<IRepository>();
            _userId = Guid.NewGuid();

            _identity.Stub(i => i.Name).Return(_userId.ToString());
            Thread.CurrentPrincipal = new FohjinPrincipal(_identity);

            _behavior = new set_the_current_logged_in_user_on_the_output_viewmodel(_repo);
        }
        
        [TearDown]
        public void TearDown()
        {
            Thread.CurrentPrincipal = _previousPrin;
        }

        [Test]
        public void should_not_do_anything_if_no_principal_is_loaded()
        {
            Thread.CurrentPrincipal = _previousPrin;

            var model = new TestViewModel();
            _behavior.ModifyOutput(model);

            model.CurrentUser.ShouldBeNull();
        }

        [Test]
        public void should_load_the_user_from_the_id_on_the_principal()
        {
            var model = new TestViewModel();
            _behavior.PrepareInput(model);

            _repo.AssertWasCalled(r => r.Load<User>(_userId));
        }
    }
}