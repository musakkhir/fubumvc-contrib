namespace FubuMVC.Validation
{
    public interface IValidationRule
    {
        bool Validate(object value);
    }
}