using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Tests;
using FubuMVC.Validation.Behaviors;
using FubuMVC.Validation.DSL;
using FubuMVC.Validation.Tests.ViewModels;
using FubuMVC.Validation.Validators;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests
{
    [TestFixture]
    public class BehaviorTests
    {
        private IValidatorConfiguration _validatorConfiguration;
        private ValidationDSL _validationDSL;

        [SetUp]
        public void SetUp()
        {
            _validatorConfiguration = new ValidatorConfiguration();
            _validationDSL = new ValidationDSL(_validatorConfiguration);

            Expression<Func<PropertyInfo, bool>> propertySelector1 = property => !property.Name.Contains("Optional");
            Expression<Func<PropertyInfo, bool>> propertySelector2 = property => property.Name.StartsWith("Email");
            Expression<Func<PropertyInfo, bool>> propertySelector3 = property => property.Name.StartsWith("Url");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector1)
                .WillBeValidatedBy<IsRequired>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector2)
                .WillBeValidatedBy<IsEmail>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector3)
                .WillBeValidatedBy<IsUrl>());

            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t == typeof(ValidationTestSUT)));
        }

        [Test]
        public void Should_be_able_to_use_the_bahavior_to_validate_an_input_model()
        {
            ValidationTestSUT viewModel = new ValidationTestSUT
            {
                AnyString = "Any String",
                AnyStringOptional = "",
                Email = "Mark.Nijhof@Gmail.com",
                EmailOptional = "",
                Url = "www.nijhof.com",
                UrlOptional = ""
            };

            var behavior = new convention_based_input_viewmodel_validation(_validatorConfiguration);
            behavior.PrepareInput(viewModel);

            viewModel.GetInvalidFields().Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_use_the_bahavior_to_validate_an_input_model_and_fail_because_AnyString_IsRequired()
        {
            ValidationTestSUT viewModel = new ValidationTestSUT
            {
                AnyString = "",
                AnyStringOptional = "",
                Email = "Mark.Nijhof@Gmail.com",
                EmailOptional = "",
                Url = "www.nijhof.com",
                UrlOptional = ""
            };

            var behavior = new convention_based_input_viewmodel_validation(_validatorConfiguration);
            behavior.PrepareInput(viewModel);

            viewModel.GetInvalidFields().Count().ShouldEqual(1);
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().First()).First().ShouldEqual(typeof(IsRequired));
        }

        [Test]
        public void Should_be_able_to_use_the_bahavior_to_validate_an_input_model_and_fail_because_Email_IsRequired()
        {
            ValidationTestSUT viewModel = new ValidationTestSUT
            {
                AnyString = "Any String",
                AnyStringOptional = "",
                Email = "",
                EmailOptional = "",
                Url = "www.nijhof.com",
                UrlOptional = ""
            };

            var behavior = new convention_based_input_viewmodel_validation(_validatorConfiguration);
            behavior.PrepareInput(viewModel);

            viewModel.GetInvalidFields().Count().ShouldEqual(1);
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().First()).First().ShouldEqual(typeof(IsRequired));
        }

        [Test]
        public void Should_be_able_to_use_the_bahavior_to_validate_an_input_model_and_fail_because_Email_and_AnyString_IsRequired()
        {
            ValidationTestSUT viewModel = new ValidationTestSUT
            {
                AnyString = "",
                AnyStringOptional = "",
                Email = "",
                EmailOptional = "",
                Url = "www.nijhof.com",
                UrlOptional = ""
            };

            var behavior = new convention_based_input_viewmodel_validation(_validatorConfiguration);
            behavior.PrepareInput(viewModel);

            viewModel.GetInvalidFields().Count().ShouldEqual(2);
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().First()).First().ShouldEqual(typeof(IsRequired));
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().Last()).First().ShouldEqual(typeof(IsRequired));
        }

        [Test]
        public void Should_be_able_to_use_the_bahavior_to_validate_an_input_model_and_fail_because_Email_and_Url_are_misformed()
        {
            ValidationTestSUT viewModel = new ValidationTestSUT
            {
                AnyString = "Any String",
                AnyStringOptional = "",
                Email = "Mark.NijhofGmail.com",
                EmailOptional = "",
                Url = "www.nijhofcom",
                UrlOptional = ""
            };

            var behavior = new convention_based_input_viewmodel_validation(_validatorConfiguration);
            behavior.PrepareInput(viewModel);

            viewModel.GetInvalidFields().Count().ShouldEqual(2);
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().First()).First().ShouldEqual(typeof(IsEmail));
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().Last()).First().ShouldEqual(typeof(IsUrl));
        }

        [Test]
        public void Should_be_able_to_use_the_bahavior_to_validate_an_input_model_and_fail_because_EmailOptional_and_UrlOptional_are_misformed()
        {
            ValidationTestSUT viewModel = new ValidationTestSUT
            {
                AnyString = "Any String",
                AnyStringOptional = "",
                Email = "Mark.Nijhof@Gmail.com",
                EmailOptional = "Mark.NijhofGmail.com",
                Url = "www.nijhof.com",
                UrlOptional = "www.nijhofcom"
            };

            var behavior = new convention_based_input_viewmodel_validation(_validatorConfiguration);
            behavior.PrepareInput(viewModel);

            viewModel.GetInvalidFields().Count().ShouldEqual(2);
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().First()).First().ShouldEqual(typeof(IsEmail));
            viewModel.GetBrokenRulesFor(viewModel.GetInvalidFields().Last()).First().ShouldEqual(typeof(IsUrl));
        }
    }
}