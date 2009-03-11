using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.DSL
{
    public class RuleExpression
    {
        private readonly IValidatorConfiguration _validatorConfiguration;
        private readonly Expression<Func<PropertyInfo, bool>> _propertySelector;

        public RuleExpression(IValidatorConfiguration validatorConfiguration, Expression<Func<PropertyInfo, bool>> propertySelector)
        {
            _validatorConfiguration = validatorConfiguration;
            _propertySelector = propertySelector;
        }

        public RuleExpression WillBeValidatedBy<TValidatorRule>() where TValidatorRule : IValidationRule, new()
        {
            _validatorConfiguration.AddDefaultPropertyConvention(_propertySelector).AddRule<TValidatorRule>();
            return this;
        }
    }
}