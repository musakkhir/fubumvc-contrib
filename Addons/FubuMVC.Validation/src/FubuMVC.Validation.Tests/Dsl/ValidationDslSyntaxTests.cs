using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.Dsl
{
    [TestFixture]
    public class ValidationDslSyntaxTests
    {
        [Test]
        public void Should_be_able_to_write_configure_default_conventions_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.ByDefault
                    .PropertiesMatching(p => p.Name.StartsWith("Email"), r => {
                        r.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>();
                        r.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>();
                    });
            };
        }

        [Test]
        public void Should_be_able_to_write_configure_default_conventions_using_the_dsl_with_a_two_contructor_rule()
        {
            ValidationConfig.Configure = x =>
            {
                x.ByDefault
                    .PropertiesMatching(p => p.Name.StartsWith("Email"), r => 
                        r.WillBeValidatedBy<IsValidCaptcha<CanBeAnyViewModel>>(p =>
                            p.NeedsAdditionalPropertyMatching(y => y.Name.StartsWith("Email"))));
            };
        }

        [Test]
        public void Should_be_able_to_write_scanning_assemblies_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.AddViewModelsFromAssembly
                    .ContainingType<TestViewModel>()
                    .Where(t => t.Namespace.EndsWith("Tests.Helper"));
            };
        }

        [Test]
        public void Should_be_able_to_write_overriding_a_specific_property_adding_validation_rules_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.OverrideConfigFor<TestViewModel>()
                    .Property(p => p.Valid_Email, r =>
                    {
                        r.WillBeValidatedBy<IsRequired<TestViewModel>>();
                        r.WillBeValidatedBy<IsEmail<TestViewModel>>();
                    });
            };
        }

        [Test]
        public void Should_be_able_to_write_overriding_a_specific_property_adding_validation_rules_using_the_dsl_with_a_two_contructor_rule_with_specific_aditional_properties()
        {
            ValidationConfig.Configure = x =>
            {
                x.OverrideConfigFor<TestViewModel>()
                    .Property(p => p.Valid_Email, r =>
                        r.WillBeValidatedBy<IsValidCaptcha<TestViewModel>>(p =>
                            p.NeedsAdditionalProperty(y => y.False_Email_1)));
            };
        }

        [Test]
        public void Should_be_able_to_write_overriding_default_conventions_removing_validation_rules_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.OverrideConfigFor<TestViewModel>()
                    .WillNotBeValidatedBy<IsRequired<TestViewModel>>()
                    .WillNotBeValidatedBy<IsEmail<TestViewModel>>();
            };
        }

        [Test]
        public void Should_be_able_to_write_overriding_a_specific_property_adding_and_removing_validation_rules_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.OverrideConfigFor<TestViewModel>()
                    .WillNotBeValidatedBy<IsRequired<TestViewModel>>()
                    .Property(p => p.Valid_Email, r => 
                        r.WillBeValidatedBy<IsEmail<TestViewModel>>());
            };
        }

        [Test]
        public void Should_be_able_to_write_overriding_a_specific_property_removing_all_validation_rules_from_property_convention_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.OverrideConfigFor<TestViewModel>()
                    .Property(p => p.Valid_Email, r =>
                        r.ClearValidationRules());
            };
        }

        [Test]
        public void Should_be_able_to_write_overriding_default_conventions_removing_all_validation_rules_from_view_model_convention_using_the_dsl()
        {
            ValidationConfig.Configure = x =>
            {
                x.OverrideConfigFor<TestViewModel>()
                    .WillNotBeValidated();
            };
        }
    }
}