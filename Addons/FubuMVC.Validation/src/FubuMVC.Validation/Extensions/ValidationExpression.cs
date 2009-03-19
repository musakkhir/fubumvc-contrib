using System;
using System.Linq.Expressions;
using FubuMVC.Core.View;
using FubuMVC.Validation.Results;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Extensions
{
    public class ValidationExpression<TViewModel> where TViewModel : class
    {
        private readonly IFubuView<TViewModel> _viewModel;
        private readonly string _propertySelectorString;
        private string _invalidDisplayString;
        private string _validDisplayString;

        public ValidationExpression(IFubuView<TViewModel> viewModel, Expression<Func<TViewModel, object>> propertySelector)
        {
            _viewModel = viewModel;
            _propertySelectorString = new UglyExpressionConvertor().ToString(propertySelector);
            _invalidDisplayString = string.Empty;
            _validDisplayString = string.Empty;
        }

        public ValidationExpression<TViewModel> DisplayWhenInvalid(string displayString)
        {
            _invalidDisplayString = displayString;
            return this;
        }

        public ValidationExpression<TViewModel> DisplayWhenValid(string displayString)
        {
            _validDisplayString = displayString;
            return this;
        }

        public override string ToString()
        {
            var iCanBeValidatedViewModel = _viewModel.Model as ICanBeValidated;

            if (iCanBeValidatedViewModel != null )
                return iCanBeValidatedViewModel.ValidationResults.IsValid(_propertySelectorString)
                           ? _validDisplayString
                           : _invalidDisplayString;

            return string.Empty;
        }
    }
}