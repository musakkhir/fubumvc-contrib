using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Behaviors
{
    public interface IValidate
    {
        IValidationResults Validate<TViewModel>(TViewModel viewModel) where TViewModel : class;
    }
}