namespace FubuMVC.Validation.Results
{
    public interface ICanBeValidated
    {
        IValidationResults ValidationResults { get; }
    }
}