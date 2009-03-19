namespace FubuMVC.Validation.Rules
{
    public interface IValidationRule<TViewModel>
    {
        bool IsValid(TViewModel viewModel);
        string PropertyFilter { get; }
    }
}