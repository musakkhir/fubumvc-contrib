using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Rules
{
    public class IsEmail<TViewModel> : IValidationRule<TViewModel> 
    {
        private const string regexPattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                            + @"[a-zA-Z]{2,}))$";

        private readonly Expression<Func<TViewModel, string>> _propToValidateExpression;

        public IsEmail(Expression<Func<TViewModel, string>> propToValidateExpression)
        {
            _propToValidateExpression = propToValidateExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_propToValidateExpression);
        }

        public bool IsValid(TViewModel viewModel)
        {
            var value = _propToValidateExpression.Compile().Invoke(viewModel);
            return string.IsNullOrEmpty(value) || new Regex(regexPattern).IsMatch(value);
        }

        public string PropertyFilter { get; private set; }
    }
}