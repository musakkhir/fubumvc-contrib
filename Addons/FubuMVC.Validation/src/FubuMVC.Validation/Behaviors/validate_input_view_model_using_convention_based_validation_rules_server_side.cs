using FubuMVC.Core.Behaviors;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Behaviors
{
    public class validate_input_view_model_using_convention_based_validation_rules_server_side : behavior_base_for_convenience
    {
        private readonly IValidate _validate;

        public validate_input_view_model_using_convention_based_validation_rules_server_side(IValidate validate)
        {
            _validate = validate;
        }

        public override void PrepareInput<INPUT>(INPUT input)
        {
            if (!(input is ICanBeValidated)) return;

            var method = _validate.GetType().GetMethod("Validate");
            var genericMethod = method.MakeGenericMethod(input.GetType());

            IValidationResults validationResults = (IValidationResults)genericMethod.Invoke(_validate, new object[] { input });

            ((ICanBeValidated)input).ValidationResults.CloneFrom(validationResults);
        }
    }
}