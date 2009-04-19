using System;
using System.Linq.Expressions;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ExtendedRuleExpression<TViewModel> where TViewModel : class
    {
        private readonly ValidationConfiguration _validationConfiguration;
        private readonly Expression<Func<TViewModel, object>> _property;

        public ExtendedRuleExpression(ValidationConfiguration validationConfiguration, Expression<Func<TViewModel, object>> property)
        {
            _validationConfiguration = validationConfiguration;
            _property = property;
        }

        public void WillBeValidatedBy<TValidationRule>() where TValidationRule : IValidationRule<TViewModel>
        {
            var properties = new AdditionalProperties();
            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();

            _validationConfiguration.DiscoveredTypes.AddRuleFor(_property, new ValidationRuleSetup(validationRuleType, properties));
        }

        public void WillBeValidatedBy<TValidationRule>(Action<ExtendedAdditionalPropertyExpression<TViewModel>> additionalProperties) where TValidationRule : IValidationRule<TViewModel>
        {
            var properties = new AdditionalProperties();
            var additionalPropertyExpression = new ExtendedAdditionalPropertyExpression<TViewModel>(properties);
            additionalProperties(additionalPropertyExpression);

            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();

            _validationConfiguration.DiscoveredTypes.AddRuleFor(_property, new ValidationRuleSetup(validationRuleType, properties));
        }

        public void ClearValidationRules()
        {
            _validationConfiguration.DiscoveredTypes.RemoveAllRulesFor<TViewModel>();
        }
    }
}