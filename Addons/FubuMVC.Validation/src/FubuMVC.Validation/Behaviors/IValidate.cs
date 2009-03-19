using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Behaviors
{
    public interface IValidate
    {
        void Validate<TViewModel>(TViewModel viewModel) where TViewModel : ICanBeValidated;
    }
}