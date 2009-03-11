using FubuMVC.Core.Behaviors;
using FubuMVC.Validation.DSL;

namespace FubuMVC.Validation.Behaviors
{
    public class convention_based_input_viewmodel_validation : behavior_base_for_convenience
    {
        private readonly IValidatorConfiguration _validatorConfiguration;

        public convention_based_input_viewmodel_validation(IValidatorConfiguration validatorConfiguration)
        {
            _validatorConfiguration = validatorConfiguration;
        }

        public override void PrepareInput<INPUT>(INPUT input)
        {
            if (input is ICanBeValidated)
                _validatorConfiguration.Validate(input as ICanBeValidated);
        }
    }
}