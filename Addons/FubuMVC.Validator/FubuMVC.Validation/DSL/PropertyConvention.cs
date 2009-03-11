using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Core;

namespace FubuMVC.Validation.DSL
{
    public class PropertyConvention
    {
        private readonly IList<IValidationRule> _rules;

        public PropertyConvention(Expression<Func<PropertyInfo, bool>> property)
        {
            Property = property;
            _rules = new List<IValidationRule>();
        }

        public Expression<Func<PropertyInfo, bool>> Property { get; private set; }

        public void AddRule<TValidatorRule>() where TValidatorRule : IValidationRule, new()
        {
            IValidationRule validationRule = new TValidatorRule();

            if (_rules.Where(rule => rule.GetType() == validationRule.GetType()).FirstOrDefault() == null)
                _rules.Add(validationRule);
        }

        public IEnumerable<IValidationRule> GetRules()
        {
            return _rules.AsEnumerable();
        }
    }
}