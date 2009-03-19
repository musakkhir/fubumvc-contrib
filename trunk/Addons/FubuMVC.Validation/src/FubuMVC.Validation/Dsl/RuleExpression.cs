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
    }
}