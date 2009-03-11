using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Tests;
using FubuMVC.Validation.DSL;
using FubuMVC.Validation.Tests.ViewModels;
using FubuMVC.Validation.Validators;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests
{
    [TestFixture]
    public class ValidationConfigTest
    {
        private ValidatorConfiguration _validatorConfiguration;
        private ValidationDSL _validationDSL;

        [SetUp]
        public void SetUp()
        {
            _validatorConfiguration = new ValidatorConfiguration();
            _validationDSL = new ValidationDSL(_validatorConfiguration);
        }

        [Test]
        public void Should_be_able_to_use_the_following_syntax_to_add_validation_rules_to_the_configuration()
        {
            ValidationConfig.Configure = x => x.ByDefault.EveryViewModel(r =>
            {
                r.Property(property => !property.Name.Contains("Optional"))
                    .WillBeValidatedBy<IsRequired>();
                r.Property(property => property.Name.StartsWith("Email"))
                    .WillBeValidatedBy<IsRequired>()
                    .WillBeValidatedBy<IsEmail>();
                r.Property(property => property.Name.StartsWith("Url"))
                    .WillBeValidatedBy<IsUrl>();
            });
        }

        [Test]
        public void Should_store_the_added_validation_rule_to_the_validator_configuration()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector = property => !property.Name.Contains("Optional");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector).WillBeValidatedBy<IsRequired>());

            _validatorConfiguration.GetDefaultConventions().Count().ShouldEqual(1);
            _validatorConfiguration.GetDefaultConventions().First().Property.ShouldEqual(propertySelector);
            _validatorConfiguration.GetDefaultConventions().First().GetRules().Count().ShouldEqual(1);
            _validatorConfiguration.GetDefaultConventions().First().GetRules().First().GetType().ShouldEqual(typeof(IsRequired));
        }

        [Test]
        public void Should_not_store_duplicate_validation_rules_to_the_validator_configuration()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector = property => !property.Name.Contains("Optional");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector)
                .WillBeValidatedBy<IsRequired>()
                .WillBeValidatedBy<IsRequired>());

            _validatorConfiguration.GetDefaultConventions().Count().ShouldEqual(1);
            _validatorConfiguration.GetDefaultConventions().First().GetRules().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_store_different_validation_rules_to_the_validator_configuration()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector1 = property => !property.Name.Contains("Optional");
            Expression<Func<PropertyInfo, bool>> propertySelector2 = property => property.Name.StartsWith("Email");
            Expression<Func<PropertyInfo, bool>> propertySelector3 = property => property.Name.StartsWith("Url");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector1)
                .WillBeValidatedBy<IsRequired>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector2)
                .WillBeValidatedBy<IsEmail>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector3)
                .WillBeValidatedBy<IsUrl>());

            _validatorConfiguration.GetDefaultConventions().Count().ShouldEqual(3);
            _validatorConfiguration.GetDefaultConventions().First().GetRules().First().GetType().ShouldEqual(typeof(IsRequired));
            _validatorConfiguration.GetDefaultConventions().ToList()[1].GetRules().First().GetType().ShouldEqual(typeof(IsEmail));
            _validatorConfiguration.GetDefaultConventions().Last().GetRules().First().GetType().ShouldEqual(typeof(IsUrl));
        }

        [Test]
        public void Should_not_store_duplicate_validator_configuration()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector1 = property => !property.Name.Contains("Optional");
            Expression<Func<PropertyInfo, bool>> propertySelector2 = property => !property.Name.Contains("Optional");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector1)
                .WillBeValidatedBy<IsRequired>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector2)
                .WillBeValidatedBy<IsEmail>());

            _validatorConfiguration.GetDefaultConventions().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_store_different_validation_rules_to_the_same_validator_configuration()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector = property => !property.Name.Contains("Optional");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector)
                .WillBeValidatedBy<IsRequired>()
                .WillBeValidatedBy<IsEmail>()
                .WillBeValidatedBy<IsUrl>());

            _validatorConfiguration.GetDefaultConventions().Count().ShouldEqual(1);
            _validatorConfiguration.GetDefaultConventions().First().GetRules().ToList()[0].GetType().ShouldEqual(typeof(IsRequired));
            _validatorConfiguration.GetDefaultConventions().First().GetRules().ToList()[1].GetType().ShouldEqual(typeof(IsEmail));
            _validatorConfiguration.GetDefaultConventions().First().GetRules().ToList()[2].GetType().ShouldEqual(typeof(IsUrl));
        }

        [Test]
        public void Should_be_able_to_use_the_following_syntax_for_automatically_adding_viewmodels()
        {
            ValidationConfig.Configure = x => x.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t.Namespace.EndsWith("Tests.ViewModels")));
        }

        [Test]
        public void Should_find_all_classes_that_inherit_from_ICanBeValidated()
        {
            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t.Namespace.EndsWith("Tests.ViewModels")));

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(4);
            _validatorConfiguration.GetDiscoveredTypes().ToList()[0].ViewModel.ShouldEqual(typeof(ValidationTestSUT));
            _validatorConfiguration.GetDiscoveredTypes().ToList()[1].ViewModel.ShouldEqual(typeof(ValidationTestSUT1));
            _validatorConfiguration.GetDiscoveredTypes().ToList()[2].ViewModel.ShouldEqual(typeof(ValidationTestSUT2));
            _validatorConfiguration.GetDiscoveredTypes().ToList()[3].ViewModel.ShouldEqual(typeof(ValidationTestSUT3));
        }

        [Test]
        public void Should_find_one_single_class_which_is_ValidationTestSUT()
        {
            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t == typeof(ValidationTestSUT)));

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().ViewModel.ShouldEqual(typeof(ValidationTestSUT));
        }

        [Test]
        public void Should_find_one_single_class_which_is_ValidationTestSUT_and_add_one_default_convention()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector = property => !property.Name.Contains("Optional");
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector).WillBeValidatedBy<IsRequired>());

            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t == typeof(ValidationTestSUT)));

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().ViewModel.ShouldEqual(typeof(ValidationTestSUT));
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_find_one_single_class_which_is_ValidationTestSUT_and_add_two_default_conventions()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector1 = property => !property.Name.Contains("Optional1");
            Expression<Func<PropertyInfo, bool>> propertySelector2 = property => !property.Name.Contains("Optional2");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector1).WillBeValidatedBy<IsRequired>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector2).WillBeValidatedBy<IsRequired>());

            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t == typeof(ValidationTestSUT)));

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().ViewModel.ShouldEqual(typeof(ValidationTestSUT));
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().Count().ShouldEqual(2);
        }

        [Test]
        public void Should_be_able_to_use_the_following_syntax_for_overriding_validation_configuration()
        {
            ValidationConfig.Configure = x => x.OverrideConfigFor<ValidationTestSUT>(config => config
                .ForConvention(property => !property.Name.Contains("Optional"))
                .AddRule<IsUrl>());
        }

        [Test]
        public void Should_be_able_to_select_an_existing_property_convention()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector1 = property => !property.Name.Contains("Optional");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector1).WillBeValidatedBy<IsRequired>());

            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t == typeof(ValidationTestSUT)));
                
            _validationDSL.OverrideConfigFor<ValidationTestSUT>(config => config
                .ForConvention(property => !property.Name.Contains("Optional"))
                .AddRule<IsUrl>());

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().ViewModel.ShouldEqual(typeof(ValidationTestSUT));
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().First().GetRules().Count().ShouldEqual(2);
        }

        [Test]
        public void Should_be_able_to_select_an_existing_property_convention_from_two_conventions()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector1 = property => !property.Name.Contains("Optional1");
            Expression<Func<PropertyInfo, bool>> propertySelector2 = property => !property.Name.Contains("Optional2");

            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector1).WillBeValidatedBy<IsRequired>());
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector2).WillBeValidatedBy<IsRequired>());

            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t =>
                    t == typeof(ValidationTestSUT)));
                
            _validationDSL.OverrideConfigFor<ValidationTestSUT>(config => config
                .ForConvention(property => !property.Name.Contains("Optional1"))
                .AddRule<IsUrl>());

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().ViewModel.ShouldEqual(typeof(ValidationTestSUT));
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().Count().ShouldEqual(2);
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().First().GetRules().Count().ShouldEqual(2);
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().Last().GetRules().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_use_the_following_syntax_for_removing_a_convention()
        {
            ValidationConfig.Configure = x => x.OverrideConfigFor<ValidationTestSUT>(config => config
                .RemoveConvention(property => !property.Name.Contains("Optional")));
        }

        [Test]
        public void Should_be_able_to_remove_a_convention_from_a_viewmodel()
        {
            Expression<Func<PropertyInfo, bool>> propertySelector = property => !property.Name.Contains("Optional");
            _validationDSL.ByDefault.EveryViewModel(r => r.Property(propertySelector).WillBeValidatedBy<IsRequired>());

            _validationDSL.AddViewModelsFromAssembly
                .ContainingType<ValidationTestSUT>(c => c.Where(t => t == typeof(ValidationTestSUT)));

            _validationDSL.OverrideConfigFor<ValidationTestSUT>(p => p.RemoveConvention(property => !property.Name.Contains("Optional")));

            _validatorConfiguration.GetDiscoveredTypes().Count().ShouldEqual(1);
            _validatorConfiguration.GetDiscoveredTypes().First().ViewModel.ShouldEqual(typeof(ValidationTestSUT));
            _validatorConfiguration.GetDiscoveredTypes().First().GetConventions().Count().ShouldEqual(0);
        }
    }
}