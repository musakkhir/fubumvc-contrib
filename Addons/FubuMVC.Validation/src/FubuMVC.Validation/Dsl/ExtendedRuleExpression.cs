using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Validation.Results;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ExtendedRuleExpression<TViewModel> where TViewModel : ICanBeValidated
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
            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();
            _validationConfiguration.AddRuleFor<TViewModel>(_propertyFilter, validationRuleType);
        }

        public void WillNotBeValidated()
        {
            _validationConfiguration.RemoveAllRulesFor<TViewModel>();
        }
    }
}