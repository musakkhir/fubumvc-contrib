using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.DSL
{
    public class PropertyExpression
    {
        private readonly IValidatorConfiguration _validatorConfiguration;

        public PropertyExpression(IValidatorConfiguration validatorConfiguration)
        {
            _validatorConfiguration = validatorConfiguration;
        }

        public RuleExpression Property(Expression<Func<PropertyInfo, bool>> propertySelector)
        {
            return new RuleExpression(_validatorConfiguration, propertySelector);
        }
    }
}