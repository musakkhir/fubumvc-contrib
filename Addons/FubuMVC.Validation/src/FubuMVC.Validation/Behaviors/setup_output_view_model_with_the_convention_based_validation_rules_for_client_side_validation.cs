using FubuMVC.Core.Behaviors;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Behaviors
{
    public class setup_output_view_model_with_the_convention_based_validation_rules_for_client_side_validation : behavior_base_for_convenience
    {
        private readonly IValidate _validate;

        public setup_output_view_model_with_the_convention_based_validation_rules_for_client_side_validation(IValidate validate)
        {
            _validate = validate;
        }

        public override void ModifyOutput<OUTPUT>(OUTPUT output)
        {
            if (!(output is ICanBeValidated)) return;

            var method = _validate.GetType().GetMethod("SetupViewModel");
            var genericMethod = method.MakeGenericMethod(output.GetType());

            genericMethod.Invoke(_validate, new object[] { output });
        }
    }
}