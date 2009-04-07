using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Behaviors
{
    public interface IValidate
    {
        IValidationResults<TViewModel> Validate<TViewModel>(TViewModel viewModel) where TViewModel : class;
    }
}