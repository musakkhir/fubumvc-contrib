using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Controller.Results;
using FubuMVC.Core.Security;
using FubuMVC.Core.SessionState;
using FubuMVC.Tests;
using FubuTask.Presentation.Controllers;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuTask.Tests.Presentation.Controllers
{
    [TestFixture]
    public class when_loading_the_login_screen_for_the_first_time : the_login_controller
    {
        [Test]
        public void should_attempt_to_load_flash()
        {
            _controller.Index(null);

            _flash.AssertWasCalled(f => f.Retrieve<LoginAttemptViewModel>());
        }

        [Test]
        public void should_return_an_empty_viewmodel()
        {
            _controller.Index(null).ShouldNotBeNull();
        }
    }

    [TestFixture]
    public class when_loading_the_login_screen_after_a_login_failure : the_login_controller
    {
        [Test]
        public void should_return_model_from_flash()
        {
            var model = new LoginAttemptViewModel();
            _flash.Stub(f => f.Retrieve<LoginAttemptViewModel>()).Return(model);

            _controller.Index(null).ShouldBeTheSameAs(model);
        }
    }

    [TestFixture]
    public class when_attempting_to_login_with_valid_credentials : the_login_controller
    {
        private LoginAttemptViewModel _output;

        protected override void beforeEach()
        {
            _resolver.Stub(r => r.PrimaryApplicationUrl()).Return("PRIMARY");
            _output = _controller.Login(new LoginAttemptViewModel { Username = "asdf", Password = "asdf" });
        }

        [Test]
        public void should_update_the_authentication_context_for_this_user()
        {
            _auth.AssertWasCalled(a => a.ThisUserHasBeenAuthenticated("asdf", false));
        }

        [Test]
        public void should_redirect_to_home()
        {
            _output.ResultOverride.ShouldBeOfType<RedirectResult>().Url.ShouldEqual("PRIMARY");
        }
    }

    [TestFixture]
    public class when_attempting_to_login_with_invalid_credentials : the_login_controller
    {
        private LoginAttemptViewModel _output;
        private LoginAttemptViewModel _attempt;

        protected override void beforeEach()
        {
            _resolver.Stub(r => r.UrlFor<LoginController>(l => l.Index(null))).IgnoreArguments().Return("LOGIN");
            _attempt = new LoginAttemptViewModel { Username = "bogus", Password = "bogus" };
            _output = _controller.Login(_attempt);
        }

        [Test]
        public void should_set_an_error()
        {
            _output.Error.ShouldNotBeNull();
        }

        [Test]
        public void should_redirect_back_to_login_screen()
        {
            _output.ResultOverride.ShouldBeOfType<RedirectResult>().Url.ShouldEqual("LOGIN");
        }

        [Test]
        public void should_flash_the_model()
        {
            _flash.AssertWasCalled(f => f.Flash(_output));
        }

        [Test]
        public void should_clear_the_password()
        {
            _output.Password.ShouldBeNull();
        }
    }

    public abstract class the_login_controller
    {
        protected IFlash _flash;
        protected IAuthenticationContext _auth;
        protected LoginController _controller;
        protected IServiceLocator _locator;
        protected IUrlResolver _resolver;

        [SetUp]
        public void SetUp()
        {
            _flash = MockRepository.GenerateStub<IFlash>();
            _auth = MockRepository.GenerateStub<IAuthenticationContext>();
            _locator = MockRepository.GenerateStub<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(()=> _locator);
            _resolver = MockRepository.GenerateStub<IUrlResolver>();
            _locator.Stub(l => l.GetInstance<IUrlResolver>()).Return(_resolver);
            _controller = new LoginController(_flash, _auth, _resolver);

            beforeEach();
        }

        protected virtual void beforeEach()
        {
        }
    }
}