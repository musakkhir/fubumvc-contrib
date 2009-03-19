using FubuMVC.Tests;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Extensions;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;
using TestView=FubuMVC.Validation.Tests.Helper.TestView;

namespace FubuMVC.Validation.Tests.Exensions
{
    [TestFixture]
    public class ValidationExtensionsTests
    {
        private TestViewModel _testViewModel;
        private ValidationConfiguration _validationConfiguration;
        private TestView _testView;

        [SetUp]
        public void SetUp()
        {
            _testViewModel = new TestViewModel();
            _validationConfiguration = new ValidationConfiguration();

            var convention0 = new DefaultPropertyConvention(x => !x.Name.StartsWith("Optional"));
            convention0.AddValidationRule<IsRequired<CanBeAnyViewModel>>();
            _validationConfiguration.AddDefaultPropertyConvention(convention0);

            var convention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            convention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            _validationConfiguration.AddDefaultPropertyConvention(convention1);

            var convention2 = new DefaultPropertyConvention(x => x.Name.Contains("Email"));
            convention2.AddValidationRule<IsEmail<CanBeAnyViewModel>>();
            _validationConfiguration.AddDefaultPropertyConvention(convention2);

            _validationConfiguration.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.Validate(_testViewModel);

            _testView = new TestView();
            _testView.SetModel(_testViewModel);
        }

        [Test]
        public void Should_be_able_to_use_the_ValidationExtensions_on_a_view_implementing_ICanBeValidated()
        {
            _testView.Validate(x => x.Valid_Email);
        }

        [Test]
        public void Should_not_show_a_message_when_the_property_is_validated_correctly()
        {
            _testView.Validate(x => x.Valid_Email)
                .DisplayWhenInvalid("Field is invalid!")
                .ToString()
                .ShouldEqual(string.Empty);
        }

        [Test]
        public void Should_show_a_message_when_the_property_is_not_validated_correctly()
        {
            _testView.Validate(x => x.False_Email)
                .DisplayWhenInvalid("Field is invalid!")
                .ToString()
                .ShouldEqual("Field is invalid!");
        }

        [Test]
        public void Should_show_a_message_when_the_property_is_validated_correctly()
        {
            _testView.Validate(x => x.Valid_Email)
                .DisplayWhenInvalid("Field is invalid!")
                .DisplayWhenValid("Field is valid!")
                .ToString()
                .ShouldEqual("Field is valid!");
        }

        [Test]
        public void Should_output_auto_navigation_script_when_view_model_is_invalid()
        {
            _testView.Validate()
                .NavigateHereWhenInvalid()
                .ToString()
                .ShouldEqual("<a name=\"invalid_validation\"></a><script language=\"javascript\">window.location.href = \"#invalid_validation\";</script>");
        }

        [Test]
        public void Should_not_output_auto_navigation_script_when_view_mode_is_valid()
        {
            var model = new ValidTestViewModel();
            _validationConfiguration.Validate(model);
            _testView.SetModel(model);

            _testView.Validate()
                .NavigateHereWhenInvalid()
                .ToString()
                .ShouldEqual(string.Empty);
        }
    }
}