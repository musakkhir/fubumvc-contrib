using System.Linq;
using FubuMVC.Tests;
using FubuMVC.Validation.Results;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Results
{
    [TestFixture]
    public class ValidationResultsTests
    {
        [Test]
        public void Should_be_able_to_Clone_a_validation_results_from_an_other_validation_results()
        {
            var isRequired = new IsRequired<TestViewModel>(x => x.Valid_Email);

            var validationResultsSource = new ValidationResults<TestViewModel>();
            validationResultsSource.AddInvalidField("test", isRequired);

            var validationResultsTarget = new ValidationResults<TestViewModel>();
            validationResultsTarget.CloneFrom(validationResultsSource);

            validationResultsTarget.GetInvalidFields().Count().ShouldEqual(1);
            validationResultsTarget.GetInvalidFields().First().ShouldEqual("test");
            validationResultsTarget.GetBrokenRulesFor("test").Count().ShouldEqual(1);
            validationResultsTarget.GetBrokenRulesFor("test").First().PropertyFilter.ShouldEqual(isRequired.PropertyFilter);
        }

        [Test]
        public void Should_be_able_to_Clone_a_validation_results_from_an_other_different_validation_results()
        {
            var isRequired = new IsRequired<TestViewModel1>(x => x.Valid_Email);

            var validationResultsSource = new ValidationResults<TestViewModel1>();
            validationResultsSource.AddInvalidField("test", isRequired);

            var validationResultsTarget = new ValidationResults<TestViewModel>();
            validationResultsTarget.CloneFrom(validationResultsSource);

            validationResultsTarget.GetInvalidFields().Count().ShouldEqual(1);
            validationResultsTarget.GetInvalidFields().First().ShouldEqual("test");
            validationResultsTarget.GetBrokenRulesFor("test").Count().ShouldEqual(1);
            validationResultsTarget.GetBrokenRulesFor("test").First().PropertyFilter.ShouldEqual(isRequired.PropertyFilter);
        }
    }
}