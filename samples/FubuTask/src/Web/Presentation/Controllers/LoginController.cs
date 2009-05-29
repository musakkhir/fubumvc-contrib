using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Controller;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Html;
using FubuMVC.Core.Security;
using FubuMVC.Core.SessionState;

namespace FubuTask.Presentation.Controllers
{
    public class LoginController
    {
        private readonly IFlash _flash;
        private readonly IAuthenticationContext _authContext;
        private readonly IUrlResolver _resolver;

        public LoginController(IFlash flash, IAuthenticationContext authContext, IUrlResolver resolver)
        {
            _flash = flash;
            _authContext = authContext;
            _resolver = resolver;
        }

        public LoginAttemptViewModel Index(ViewModel inModel)
        {
            var model = _flash.Retrieve<LoginAttemptViewModel>();

            return model ?? new LoginAttemptViewModel();
        }

        public LoginAttemptViewModel Login(LoginAttemptViewModel attempt)
        {
            var output = new LoginAttemptViewModel();

            if (attempt.Username == "asdf" && attempt.Password == "asdf")
            {
                _authContext.ThisUserHasBeenAuthenticated(attempt.Username, false);
                output.RedirectTo(_resolver.PrimaryApplicationUrl());
            }
            else
            {
                output.Error = "Invalid username or password";
                output.Password = null;
                _flash.Flash(output);
                output.RedirectTo<LoginController>(c => c.Index(null));
            }
            return output;
        }
    }

    public class LoginAttemptViewModel : ISupportResultOverride
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }

        public IInvocationResult ResultOverride { get; set; }

    }
}