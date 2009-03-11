
namespace FubuMVC.Validation.Validators
{
    public class IsRequired : IValidationRule
    {
        public bool Validate(object value)
        {
            return !string.IsNullOrEmpty(value as string);
        }
    }
}