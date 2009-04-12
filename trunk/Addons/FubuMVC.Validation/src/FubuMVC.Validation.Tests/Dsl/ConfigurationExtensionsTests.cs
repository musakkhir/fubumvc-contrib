using FubuMVC.Tests;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Dsl
{
    [TestFixture]
    public class ConfigurationExtensionsTests
    {
        [Test]
        public void Should_return_true_when_a_property_was_declared_in_the_specified_view_model()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("Valid_Email");

            propertyInfo.IsDeclaredIn<TestViewModel>().ShouldBeTrue();
        }

        [Test]
        public void Should_return_false_when_a_property_was_not_declared_in_the_specified_view_model()
        {
            var testViewModel = new TestViewModel1();
            var propertyInfo = testViewModel.GetType().GetProperty("Valid_Email");

            propertyInfo.IsDeclaredIn<TestViewModel1>().ShouldBeFalse();
        }

        [Test]
        public void Should_return_true_when_the_property_name_starts_with_Valid()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("Valid_Email");

            propertyInfo.NameStartsWith("Valid").ShouldBeTrue();
        }

        [Test]
        public void Should_return_false_when_the_property_name_does_not_start_with_Valid()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("False_Email_1");

            propertyInfo.NameStartsWith("Valid").ShouldBeFalse();
        }

        [Test]
        public void Should_return_true_when_the_property_name_ends_with_Email()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("Valid_Email");

            propertyInfo.NameEndsWith("Email").ShouldBeTrue();
        }

        [Test]
        public void Should_return_false_when_the_property_name_does_not_end_with_Email()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("False_Email_1");

            propertyInfo.NameEndsWith("Valid").ShouldBeFalse();
        }

        [Test]
        public void Should_return_true_when_the_property_name_contains_Email()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("Valid_Email");

            propertyInfo.NameContains("Valid").ShouldBeTrue();
        }

        [Test]
        public void Should_return_false_when_the_property_name_does_not_contain_url()
        {
            var testViewModel = new TestViewModel();
            var propertyInfo = testViewModel.GetType().GetProperty("False_Email_1");

            propertyInfo.NameContains("Url").ShouldBeFalse();
        }

        [Test]
        public void Should_return_true_because_the_view_model_implements_ICanBeValidated()
        {
            typeof(TestViewModel).ImplementsICanBeValidated().ShouldBeTrue();
        }

        [Test]
        public void Should_return_false_because_the_view_model_does_not_implement_ICanBeValidated()
        {
            typeof(TestViewModelNotImplementingICanBeValidated).ImplementsICanBeValidated().ShouldBeFalse();
        }
    }
}