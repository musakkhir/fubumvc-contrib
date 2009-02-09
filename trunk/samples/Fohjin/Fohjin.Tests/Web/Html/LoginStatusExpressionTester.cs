using Fohjin.Core.Domain;
using Fohjin.Core.Web;
using Fohjin.Core.Web.Html;
using Fohjin.Core.Web.WebForms;
using FubuMVC.Core.Controller.Config;
using NUnit.Framework;
using FubuMVC.Core.Html.Expressions;
using FubuMVC.Core.View.WebForms;
using Rhino.Mocks;

namespace Fohjin.Tests.Web.Html
{
    [TestFixture]
    public class LoginStatusExpressionTester
    {
        private IWebFormsViewRenderer _renderer;
        private User _authenticatedUser;
        private User _notAuthenticatedUser;
        private LoginStatusExpression _expression;
        private FubuConventions _conventions;

        [SetUp]
        public void SetUp()
        {
            _renderer = MockRepository.GenerateStub<IWebFormsViewRenderer>();
            _conventions = new FubuConventions();
            _authenticatedUser = new User{IsAuthenticated = true};
            _notAuthenticatedUser = new User{IsAuthenticated = false};
            _expression = new LoginStatusExpression(null, _renderer, _conventions);
        }

        [Test]
        public void should_use_the_logged_in_view_when_user_is_not_null_and_is_authenticated()
        {
            _expression.For(_authenticatedUser)
                .WhenLoggedInShow<TestLoggedInViewControl>()
                .RenderExpression
                    .ShouldBeOfType<RenderPartialExpression.RenderPartialForScope<TestLoggedInViewControl>>();
        }

        [Test]
        public void should_use_the_logged_out_view_when_user_is_null()
        {
            _expression.For(null)
                .WhenLoggedOutShow<TestLoggedOutViewControl>()
                .RenderExpression
                    .ShouldBeOfType<RenderPartialExpression.RenderPartialForScope<TestLoggedOutViewControl>>();
        }

        [Test]
        public void should_use_the_logged_out_view_when_user_is_not_null_and_is_not_authenticated()
        {
            _expression.For(_notAuthenticatedUser)
                .WhenLoggedOutShow<TestLoggedOutViewControl>()
                .RenderExpression
                    .ShouldBeOfType<RenderPartialExpression.RenderPartialForScope<TestLoggedOutViewControl>>();
        }

        public class TestLoggedInViewControl : FohjinUserControl<ViewModel>
        {
        }

        public class TestLoggedOutViewControl : FohjinUserControl<ViewModel>
        {
        }
    }
}