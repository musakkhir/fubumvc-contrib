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
        private readonly Dictionary<Type, AdditionalProperties> _validationRules;

        public DefaultPropertyConvention(Expression<Func<PropertyInfo, bool>> filter)
        {
            Property = new Property(filter);
            _validationRules = new Dictionary<Type, AdditionalProperties>();
        }

        public Property Property { get; private set; }

        public override string ToString()
        {
            return Property.ToString();
        }

        public void AddValidationRule<TValidationRule>() where TValidationRule : IValidationRule<CanBeAnyViewModel>
        {
            AddValidationRule<TValidationRule>(new AdditionalProperties());
        }

        public void AddValidationRule<TValidationRule>(AdditionalProperties additionalProperties) where TValidationRule : IValidationRule<CanBeAnyViewModel>
        {
            var validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition();

            if (!_validationRules.ContainsKey(validationRuleType))
                _validationRules.Add(validationRuleType, additionalProperties);
        }

        public IEnumerable<Type> GetValidationRules()
        {
            return _validationRules.Keys.AsEnumerable();
        }

        public IEnumerable<Property> GetAdditionalPropertiesForRule(Type ruleType)
        {
            return _validationRules.ContainsKey(ruleType)
                ? _validationRules[ruleType].GetProperties()
                : new List<Property>();
        }
    }
}