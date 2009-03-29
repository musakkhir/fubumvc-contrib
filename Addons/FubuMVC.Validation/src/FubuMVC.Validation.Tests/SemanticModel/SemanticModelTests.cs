using System.Linq;
using FubuMVC.Tests;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;
using FubuMVC.Validation.Tests.Helper;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests.SemanticModel
{
    [TestFixture]
    public class SemanticModelTests
    {
        private TestViewModel _testViewModel;
        private ValidationConfiguration _validationConfiguration;

        [SetUp]
        public void SetUp()
        {
            _testViewModel = new TestViewModel();
            _validationConfiguration = new ValidationConfiguration();
        }

        [Test]
        public void Should_be_able_to_validate_a_view_model_that_implements_ICanBeValidated()
        {
            _validationConfiguration.Validate(_testViewModel);
        }

        [Test]
        public void Should_be_able_to_add_default_validation_rules_for_properties_by_convention()
        {
            DefaultPropertyConvention defaultPropertyConvention = new DefaultPropertyConvention(x => x.Name.Contains(""));

            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention);

            _validationConfiguration.DefaultPropertyConventions.GetDefaultPropertyConventions().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_assign_a_property_filter_convention_to_default_property_convention()
        {
            DefaultPropertyConvention defaultPropertyConvention = new DefaultPropertyConvention(x => x.Name.StartsWith("Email"));

            defaultPropertyConvention.ToString().ShouldEqual("property => property.Name.StartsWith(\"Email\")");
        }

        [Test]
        public void Should_not_be_able_to_assign_two_default_property_convemtions_with_the_same_property_filter_conventions_to_default_property_convention()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.StartsWith("Email"));
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            DefaultPropertyConvention defaultPropertyConvention2 = new DefaultPropertyConvention(x => x.Name.StartsWith("Email"));
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention2);

            _validationConfiguration.DefaultPropertyConventions.GetDefaultPropertyConventions().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_validation_rules_to_default_property_convention()
        {
            DefaultPropertyConvention defaultPropertyConvention = new DefaultPropertyConvention(x => x.Name.Contains(""));

            defaultPropertyConvention.AddValidationRule<IsRequired<CanBeAnyViewModel>>();

            defaultPropertyConvention.GetValidationRules().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_not_be_able_to_add_duplicate_validation_rules_to_default_property_convention()
        {
            DefaultPropertyConvention defaultPropertyConvention = new DefaultPropertyConvention(x => x.Name.Contains(""));

            defaultPropertyConvention.AddValidationRule<IsRequired<CanBeAnyViewModel>>();
            defaultPropertyConvention.AddValidationRule<IsRequired<CanBeAnyViewModel>>();

            defaultPropertyConvention.GetValidationRules().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_aditional_required_properties_to_validation_rule()
        {
            DefaultPropertyConvention defaultPropertyConvention = new DefaultPropertyConvention(x => x.Name.Contains("question"));

            var properties = new AdditionalProperties();
            var expression = new AdditionalPropertyExpression(properties);
            expression.NeedsAdditionalProperty(p => p.Name.Contains("answer"));
            expression.NeedsAdditionalProperty(p => !p.Name.Contains("question"));

            defaultPropertyConvention.AddValidationRule<IsRequired<CanBeAnyViewModel>>(properties);

            defaultPropertyConvention.GetValidationRules().Count().ShouldEqual(1);
            defaultPropertyConvention.GetAdditionalPropertiesForRule(defaultPropertyConvention.GetValidationRules().First()).Count().ShouldEqual(2);
        }

        [Test]
        public void Should_not_be_able_to_add_two_equal_aditional_required_properties_to_validation_rule()
        {
            DefaultPropertyConvention defaultPropertyConvention = new DefaultPropertyConvention(x => x.Name.Contains("question"));

            var properties = new AdditionalProperties();
            var expression = new AdditionalPropertyExpression(properties);
            expression.NeedsAdditionalProperty(p => p.Name.Contains("answer"));
            expression.NeedsAdditionalProperty(p => p.Name.Contains("answer"));

            defaultPropertyConvention.AddValidationRule<IsRequired<CanBeAnyViewModel>>(properties);

            defaultPropertyConvention.GetValidationRules().Count().ShouldEqual(1);
            defaultPropertyConvention.GetAdditionalPropertiesForRule(defaultPropertyConvention.GetValidationRules().First()).Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_types_that_implement_ICanBeValidated()
        {
            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetDiscoveredTypes().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_be_able_to_add_objects_that_implement_ICanBeValidated()
        {
            _validationConfiguration.DiscoveredTypes.AddDiscoveredType(_testViewModel.GetType());

            _validationConfiguration.DiscoveredTypes.GetDiscoveredTypes().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_not_be_able_to_add_duplicate_objects_that_implement_ICanBeValidated()
        {
            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();
            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetDiscoveredTypes().Count().ShouldEqual(1);
        }

        [Test]
        public void Should_add_the_default_property_conventions_to_discovered_types_when_they_are_added()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            defaultPropertyConvention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            DefaultPropertyConvention defaultPropertyConvention2 = new DefaultPropertyConvention(x => x.Name.Contains("Email"));
            defaultPropertyConvention2.AddValidationRule<IsEmail<CanBeAnyViewModel>>();
            defaultPropertyConvention2.AddValidationRule<IsRequired<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention2);

            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(9);
        }

        [Test]
        public void Should_be_able_to_actually_validate_an_object_that_implements_ICanBeValidated_using_the_default_conventions()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            defaultPropertyConvention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            DefaultPropertyConvention defaultPropertyConvention2 = new DefaultPropertyConvention(x => x.Name.Contains("Email"));
            defaultPropertyConvention2.AddValidationRule<IsEmail<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention2);

            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.Validate(_testViewModel);
                
            _testViewModel.ValidationResults.GetInvalidFields().Count().ShouldEqual(3);
            _testViewModel.ValidationResults.GetInvalidFields().First().ShouldEqual("property => property.False_Email_1");
            _testViewModel.ValidationResults.GetInvalidFields().Last().ShouldEqual("property => property.False_Url");
            _testViewModel.ValidationResults.GetBrokenRulesFor(_testViewModel.ValidationResults.GetInvalidFields().First()).Count().ShouldEqual(1);
            _testViewModel.ValidationResults.GetBrokenRulesFor(_testViewModel.ValidationResults.GetInvalidFields().First()).First().ShouldEqual(typeof(IsEmail<TestViewModel>));
            _testViewModel.ValidationResults.GetBrokenRulesFor(_testViewModel.ValidationResults.GetInvalidFields().Last()).Count().ShouldEqual(1);
            _testViewModel.ValidationResults.GetBrokenRulesFor(_testViewModel.ValidationResults.GetInvalidFields().Last()).First().ShouldEqual(typeof(IsUrl<TestViewModel>));
        }

        [Test]
        public void Should_add_a_validation_rule_to_a_scanned_ICanBeValidated_implementation()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            defaultPropertyConvention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(3);

            _validationConfiguration.DiscoveredTypes.AddRuleFor<TestViewModel>(x => x.Name.Contains("Url"), new ValidationRuleSetup(typeof(IsRequired<>), new AdditionalProperties()));

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(6);
        }

        [Test]
        public void Should_not_add_duplicate_validation_rules_to_a_scanned_ICanBeValidated_implementation()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            defaultPropertyConvention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(3);

            _validationConfiguration.DiscoveredTypes.AddRuleFor<TestViewModel>(x => x.Name.Contains("Url"), new ValidationRuleSetup(typeof(IsUrl<>), new AdditionalProperties()));

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(3);
        }

        [Test]
        public void Should_remove_a_validation_rule_from_a_scanned_ICanBeValidated_implementation()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            defaultPropertyConvention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            defaultPropertyConvention1.AddValidationRule<IsEmail<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(6);

            _validationConfiguration.DiscoveredTypes.RemoveRuleFrom<TestViewModel, IsUrl<TestViewModel>>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(3);
        }

        [Test]
        public void Should_remove_all_validation_rules_from_a_scanned_ICanBeValidated_implementation()
        {
            DefaultPropertyConvention defaultPropertyConvention1 = new DefaultPropertyConvention(x => x.Name.Contains("Url"));
            defaultPropertyConvention1.AddValidationRule<IsUrl<CanBeAnyViewModel>>();
            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention1);

            _validationConfiguration.DiscoveredTypes.AddDiscoveredType<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(3);

            _validationConfiguration.DiscoveredTypes.RemoveAllRulesFor<TestViewModel>();

            _validationConfiguration.DiscoveredTypes.GetRulesFor(new TestViewModel()).Count().ShouldEqual(0);
        }
    }
}