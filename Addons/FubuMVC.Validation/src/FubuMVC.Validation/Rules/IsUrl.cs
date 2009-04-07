using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Rules
{
    public class IsUrl<TViewModel> : IValidationRule<TViewModel> 
    {
        private const string regexPattern = @"^(https?://)"
                                            + @"?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@ 
                                            + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184 
                                            + @"|" // allows either IP or domain 
                                            + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www. 
                                            + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain 
                                            + @"[a-z]{2,6})" // first level domain- .com or .museum 
                                            + @"(:[0-9]{1,4})?" // port number- :80 
                                            + @"((/?)|" // a slash isn't required if there is no file name 
                                            + @"(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";

        private readonly Expression<Func<TViewModel, string>> _propToValidateExpression;

        public IsUrl(Expression<Func<TViewModel, string>> propToValidateExpression)
        {
            ConstructorArguments = new List<object> { propToValidateExpression };
            _propToValidateExpression = propToValidateExpression;
            PropertyFilter = new UglyExpressionConvertor().ToString(_propToValidateExpression);
        }

        public bool IsValid(TViewModel viewModel)
        {
            var value = _propToValidateExpression.Compile().Invoke(viewModel);
            return string.IsNullOrEmpty(value) || new Regex(regexPattern).IsMatch(value);
        }

        public string PropertyFilter { get; private set; }
        public IList<object> ConstructorArguments { get; private set; }
    }
}