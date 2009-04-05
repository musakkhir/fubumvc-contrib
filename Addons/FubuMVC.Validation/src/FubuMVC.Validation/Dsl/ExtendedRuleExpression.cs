using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ExtendedRuleExpression<TViewModel> where TViewModel : class
    {
        private readonly ValidationConfiguration _validationConfiguration;
        private readonly Expression<Func<PropertyInfo, bool>> _propertyFilter;

        public ExtendedRuleExpression(ValidationConfiguration validationConfiguration, Expression<Func<PropertyInfo, bool>> propertyFilter)
        {
            _validationConfiguration = validationConfiguration;
            _propertyFilter = propertyFilter;
        }

        public void WillBeValidatedBy<TValidationRule>() where TValidationRule : IValidationRule<TViewModel>
        {
            var properties = new AdditionalProperties();
            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();
            _validationConfiguration.DiscoveredTypes.AddRuleFor<TViewModel>(_propertyFilter, new ValidationRuleSetup(validationRuleType, properties));
        }

        public void WillBeValidatedBy<TValidationRule>(Action<AdditionalPropertyExpression> additionalProperties) where TValidationRule : IValidationRule<TViewModel>
        {
            var properties = new AdditionalProperties();
            var additionalPropertyExpression = new AdditionalPropertyExpression(properties);
            additionalProperties(additionalPropertyExpression);

            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();

            _validationConfiguration.DiscoveredTypes.AddRuleFor<TViewModel>(_propertyFilter, new ValidationRuleSetup(validationRuleType, properties));
        }

        public void WillNotBeValidated()
        {
            _validationConfiguration.DiscoveredTypes.RemoveAllRulesFor<TViewModel>();
        }
    }
}