using FubuMVC.Core.View;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Extensions
{
    public class SimpleValidationExpression<TViewModel> where TViewModel : class
    {
        private readonly IFubuView<TViewModel> _viewModel;
        private bool _shouldOutputNavigationLink;

        public SimpleValidationExpression(IFubuView<TViewModel> viewModel)
        {
            _viewModel = viewModel;
            _shouldOutputNavigationLink = false;
        }

        public SimpleValidationExpression<TViewModel> NavigateHereWhenInvalid()
        {
            _shouldOutputNavigationLink = true;
            return this;
        }

        public override string ToString()
        {
            var iCanBeValidatedViewModel = _viewModel.Model as ICanBeValidated<TViewModel>;

            if (iCanBeValidatedViewModel != null && 
                !iCanBeValidatedViewModel.ValidationResults.IsValid() && 
                _shouldOutputNavigationLink)
                return "<a name=\"invalid_validation\"></a><script language=\"javascript\">window.location.href = \"#invalid_validation\";</script>";

            return string.Empty;
        }
    }
}