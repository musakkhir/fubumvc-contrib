using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ByDefaultDslChain
    {
        private readonly ValidationConfiguration _validationConfiguration;

        public ByDefaultDslChain(ValidationConfiguration validationConfiguration)
        {
            _validationConfiguration = validationConfiguration;
        }

        public ByDefaultDslChain PropertiesMatching(Expression<Func<PropertyInfo, bool>> propertyFilter, Action<RuleExpression> rules)
        {
            var propertyConvention = _validationConfiguration.DefaultPropertyConventions.GetDefaultPropertyConventions()
                .Where(convention => convention.ToString() == new UglyExpressionConvertor().ToString(propertyFilter))
                .FirstOrDefault();

            if (propertyConvention != null)
            {
                rules(new RuleExpression(propertyConvention));
                return this;
            }

            var defaultPropertyConvention = new DefaultPropertyConvention(propertyFilter);

            rules(new RuleExpression(defaultPropertyConvention));

            _validationConfiguration.DefaultPropertyConventions.AddDefaultPropertyConvention(defaultPropertyConvention);
            return this;
        }
    }
}