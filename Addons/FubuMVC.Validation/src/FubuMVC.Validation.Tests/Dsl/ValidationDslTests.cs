using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Tests;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Dsl
{
    [TestFixture]
    public class ValidationDslTests
    {
        private ValidationConfiguration _validationConfiguration;
        private ValidationDsl _validationDsl;

        [SetUp]
        public void SetUp()
        {
            _validationConfiguration = new ValidationConfiguration();
            _validationDsl = new ValidationDsl(_validationConfiguration);
        }

        [Test]
        public void Should_store_IsRequired_rule_for_one_property_convention_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression = p => p.Name.StartsWith("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>());

            var defaultPropertyConventions = _validationConfiguration.GetDefaultPropertyConventions();
            defaultPropertyConventions.Count().ShouldEqual(1);
            defaultPropertyConventions.First().ToString().ShouldEqual("property => property.Name.StartsWith(\"Email\")");
            defaultPropertyConventions.First().Property.Match.ShouldEqual(propToValidateExpression);
            defaultPropertyConventions.First().GetValidationRules().Count().ShouldEqual(1);
            defaultPropertyConventions.First().GetValidationRules().First().ShouldEqual(typeof(IsRequired<>));
        }

        [Test]
        public void Should_store_IsRequired_and_IsEmail_rule_for_one_property_convention_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression = p => p.Name.StartsWith("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression, r =>
                {
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                    r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>();
                });

            var defaultPropertyConventions = _validationConfiguration.GetDefaultPropertyConventions();
            defaultPropertyConventions.Count().ShouldEqual(1);
            defaultPropertyConventions.First().ToString().ShouldEqual("property => property.Name.StartsWith(\"Email\")");
            defaultPropertyConventions.First().Property.Match.ShouldEqual(propToValidateExpression);
            defaultPropertyConventions.First().GetValidationRules().Count().ShouldEqual(2);
            defaultPropertyConventions.First().GetValidationRules().First().ShouldEqual(typeof(IsRequired<>));
            defaultPropertyConventions.First().GetValidationRules().Last().ShouldEqual(typeof(IsEmail<>));
        }

        [Test]
        public void Should_store_IsRequired_rule_for_two_property_conventions_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression1 = p => p.Name.StartsWith("Email");
            Expression<Func<PropertyInfo, bool>> propToValidateExpression2 = p => p.Name.StartsWith("Url");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>())
                .PropertiesMatching(propToValidateExpression2, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>());

            var defaultPropertyConventions = _validationConfiguration.GetDefaultPropertyConventions();
            defaultPropertyConventions.Count().ShouldEqual(2);
            defaultPropertyConventions.First().ToString().ShouldEqual("property => property.Name.StartsWith(\"Email\")");
            defaultPropertyConventions.First().Property.Match.ShouldEqual(propToValidateExpression1);
            defaultPropertyConventions.First().GetValidationRules().Count().ShouldEqual(1);
            defaultPropertyConventions.First().GetValidationRules().First().ShouldEqual(typeof(IsRequired<>));
            defaultPropertyConventions.Last().ToString().ShouldEqual("property => property.Name.StartsWith(\"Url\")");
            defaultPropertyConventions.Last().Property.Match.ShouldEqual(propToValidateExpression2);
            defaultPropertyConventions.Last().GetValidationRules().Count().ShouldEqual(1);
            defaultPropertyConventions.Last().GetValidationRules().First().ShouldEqual(typeof(IsRequired<>));
        }

        [Test]
        public void Should_not_add_duplicate_property_conventions_in_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression1 = p => p.Name.StartsWith("Email");
            Expression<Func<PropertyInfo, bool>> propToValidateExpression2 = p => p.Name.StartsWith("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>())
                .PropertiesMatching(propToValidateExpression2, r =>
                    r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>());

            var defaultPropertyConventions = _validationConfiguration.GetDefaultPropertyConventions();
            defaultPropertyConventions.Count().ShouldEqual(1);
            defaultPropertyConventions.First().GetValidationRules().Count().ShouldEqual(2);
            defaultPropertyConventions.First().GetValidationRules().First().ShouldEqual(typeof(IsRequired<>));
            defaultPropertyConventions.First().GetValidationRules().Last().ShouldEqual(typeof(IsEmail<>));
        }

        [Test]
        public void Should_not_add_duplicate_rules_to_property_conventions_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression = p => p.Name.StartsWith("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression, r =>
                {
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                });

            var defaultPropertyConventions = _validationConfiguration.GetDefaultPropertyConventions();
            defaultPropertyConventions.Count().ShouldEqual(1);
            defaultPropertyConventions.First().GetValidationRules().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_automatically_add_all_implementers_of_ICanBeValidated_of_a_given_assembly_using_the_ValidationDsl()
        {
            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            var propertyConventions = _validationConfiguration.GetDiscoveredTypes();
            propertyConventions.Count().ShouldEqual(7);
        }

        [Test]
        public void Should_be_able_to_automatically_add_all_implementers_of_ICanBeValidated_of_a_given_assembly_and_attach_default_validation_rules_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression = p => p.Name.Contains("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression, r =>
                {
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                    r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>();
                });

            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            var defaultPropertyConventions = _validationConfiguration.GetRulesFor(new TestViewModel());
            defaultPropertyConventions.Count().ShouldEqual(6);
        }

        [Test]
        public void Should_be_able_to_remove_a_rules_from_a_property_convention_in_ValidationConfiguration_when_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression = p => p.Name.Contains("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression, r =>
                {
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                    r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>();
                });

            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            _validationDsl.OverrideConfigFor<TestViewModel>()
                .WillNotBeValidatedBy<IsRequired<TestViewModel>>();

            var defaultPropertyConventions = _validationConfiguration.GetRulesFor(new TestViewModel());
            defaultPropertyConventions.Count().ShouldEqual(3);
        }

        [Test]
        public void Should_be_able_to_remove_all_rules_from_a_property_convention_in_ValidationConfiguration_when_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression1 = p => p.Name.StartsWith("Email");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression1, r =>
                {
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                    r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>();
                });

            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            _validationDsl.OverrideConfigFor<TestViewModel>()
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillNotBeValidated());

            var defaultPropertyConventions = _validationConfiguration.GetRulesFor(new TestViewModel());
            defaultPropertyConventions.Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_remove_all_property_conventions_in_ValidationConfiguration_when_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression1 = p => p.Name.StartsWith("Email");
            Expression<Func<PropertyInfo, bool>> propToValidateExpression2 = p => p.Name.StartsWith("Url");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>())
                .PropertiesMatching(propToValidateExpression2, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>());

            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            _validationDsl.OverrideConfigFor<TestViewModel>()
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillNotBeValidated());

            var defaultPropertyConventions = _validationConfiguration.GetRulesFor(new TestViewModel());
            defaultPropertyConventions.Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_remove_all_property_conventions_from_a_type_in_ValidationConfiguration_when_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression1 = p => p.Name.StartsWith("Email");
            Expression<Func<PropertyInfo, bool>> propToValidateExpression2 = p => p.Name.StartsWith("Url");

            _validationDsl.ByDefault
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>())
                .PropertiesMatching(propToValidateExpression2, r =>
                    r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>());

            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            _validationDsl.OverrideConfigFor<TestViewModel>()
                .WillNotBeValidated();

            var defaultPropertyConventions = _validationConfiguration.GetRulesFor(new TestViewModel());
            defaultPropertyConventions.Count().ShouldEqual(0);
        }

        [Test]
        public void Should_be_able_to_add_a_rule_in_ValidationConfiguration_when_using_the_ValidationDsl()
        {
            Expression<Func<PropertyInfo, bool>> propToValidateExpression1 = p => p.Name.Contains("Email");

            _validationDsl.AddViewModelsFromAssembly
                .ContainingType<TestViewModel>()
                .Where(t => t.Namespace.EndsWith("Tests.Helper"));

            _validationDsl.OverrideConfigFor<TestViewModel>()
                .PropertiesMatching(propToValidateExpression1, r =>
                    r.WillBeValidatedBy<IsRequired<TestViewModel>>());

            var defaultPropertyConventions = _validationConfiguration.GetRulesFor(new TestViewModel());
            defaultPropertyConventions.Count().ShouldEqual(3);
        }
    }
}