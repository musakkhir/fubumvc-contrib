using System;
using FubuMVC.Validation.Rules;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class RuleExpression
    {
        private readonly DefaultPropertyConvention _defaultPropertyConvention;

        public RuleExpression(DefaultPropertyConvention defaultPropertyConvention)
        {
            _defaultPropertyConvention = defaultPropertyConvention;
        }

        public RuleExpression WillBeValidatedBy<TValidationRule>() where TValidationRule : IValidationRule<CanBeAnyViewModel>
        {
            _defaultPropertyConvention.AddValidationRule<TValidationRule>();
            return this;
        }

        public RuleExpression WillBeValidatedBy<TValidationRule>(Action<AdditionalPropertyExpression> additionalProperties) where TValidationRule : IValidationRule<CanBeAnyViewModel>
        {
            var properties = new AdditionalProperties();
            var additionalPropertyExpression = new AdditionalPropertyExpression(properties);
            additionalProperties(additionalPropertyExpression);

            _defaultPropertyConvention.AddValidationRule<TValidationRule>(properties);
            return this;
        }
    }
}