using System;

namespace FubuMVC.Validation.DSL
{
    public class ByDefaultDslChain
    {
        private readonly IValidatorConfiguration _validatorConfiguration;

        public ByDefaultDslChain(IValidatorConfiguration validatorConfiguration)
        {
            _validatorConfiguration = validatorConfiguration;
        }

        public void EveryViewModel(Action<PropertyExpression> ruleExpression)
        {
            ruleExpression(new PropertyExpression(_validatorConfiguration));
        }
    }
}