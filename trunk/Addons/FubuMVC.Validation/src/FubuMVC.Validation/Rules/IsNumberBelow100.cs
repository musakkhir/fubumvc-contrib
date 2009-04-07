using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Rules
{
    public class IsNumberBelow100<TViewModel> : IValidationRule<TViewModel> 
    {
        private readonly Expression<Func<TViewModel, int>> _propToValidateExpression;

        public IsNumberBelow100(Expression<Func<TViewModel, int>> propToValidateExpression)
        {
            ConstructorArguments = new List<object> { propToValidateExpression };
            _propToValidateExpression = propToValidateExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_propToValidateExpression);
        }

        public bool IsValid(TViewModel viewModel)
        {
            return _propToValidateExpression.Compile().Invoke(viewModel) < 100;
        }

        public string PropertyFilter { get; private set; }
        public IList<object> ConstructorArguments { get; private set; }
    }
}