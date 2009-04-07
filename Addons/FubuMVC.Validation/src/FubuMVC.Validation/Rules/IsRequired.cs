using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Rules
{
    public class IsRequired<TViewModel> : IValidationRule<TViewModel>
    {
        private readonly Expression<Func<TViewModel, string>> _propToValidateStringExpression;
        private readonly Expression<Func<TViewModel, int>> _propToValidateIntExpression;

        public IsRequired(Expression<Func<TViewModel, string>> propToValidateExpression)
        {
            ConstructorArguments = new List<object> { propToValidateExpression };
            _propToValidateStringExpression = propToValidateExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_propToValidateStringExpression);
        }

        public IsRequired(Expression<Func<TViewModel, int>> propToValidateExpression)
        {
            ConstructorArguments = new List<object> { propToValidateExpression };
            _propToValidateIntExpression = propToValidateExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_propToValidateIntExpression);
        }

        public bool IsValid(TViewModel viewModel)
        {
            return _propToValidateStringExpression == null 
                ? IntValidator(viewModel) 
                : StringValidator(viewModel);
        }

        private bool StringValidator(TViewModel viewModel)
        {
            return !string.IsNullOrEmpty(_propToValidateStringExpression.Compile().Invoke(viewModel));
        }

        private bool IntValidator(TViewModel viewModel)
        {
            var value = _propToValidateIntExpression.Compile().Invoke(viewModel);
            return !(value == int.MinValue || value == int.MaxValue);
        }

        public string PropertyFilter { get; private set; }
        public IList<object> ConstructorArguments { get; private set; }
    }
}