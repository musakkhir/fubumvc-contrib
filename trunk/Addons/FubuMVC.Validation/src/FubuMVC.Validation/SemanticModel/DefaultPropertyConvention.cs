using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;

namespace FubuMVC.Validation.SemanticModel
{
    public class DefaultPropertyConvention
    {
        private readonly IList<Type> _validationRules;
        private readonly string _expressionString;

        public DefaultPropertyConvention(Expression<Func<PropertyInfo, bool>> filter)
        {
            Match = filter;
            _validationRules = new List<Type>();
            _expressionString = new UglyExpressionConvertor().ToString(Match);
        }

        public Expression<Func<PropertyInfo, bool>> Match { get; private set; }

        public override string ToString()
        {
            return _expressionString;
        }

        public void AddValidationRule<TValidationRule>() where TValidationRule : IValidationRule<CanBeAnyViewModel>
        {
            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();

            //if (validationRuleType.IsGenericTypeDefinition)
            //    validationRuleType = validationRuleType.GetGenericTypeDefinition();

            if (!_validationRules.Contains(validationRuleType))
                _validationRules.Add(validationRuleType);
        }

        public IEnumerable<Type> GetValidationRules()
        {
            return _validationRules.AsEnumerable();
        }
    }
}