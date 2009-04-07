namespace FubuMVC.Validation.Results
{
    public interface ICanBeValidated<TViewModel> where TViewModel : class
    {
        IValidationResults<TViewModel> ValidationResults { get; }
    }
}