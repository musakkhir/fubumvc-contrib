using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.SemanticModel
{
    public class UglyExpressionConvertor
    {
        public string ToString(Expression<Func<PropertyInfo, bool>> expression)
        {
            return ExpressionStringParser(expression.ToString());
        }

        public string ToString<TViewModel>(Expression<Func<TViewModel, string>> expression)
        {
            return ExpressionStringParser(expression.ToString());
        }

        public string ToString<TViewModel>(Expression<Func<TViewModel, int>> expression)
        {
            return ExpressionStringParser(expression.ToString());
        }

        private static string ExpressionStringParser(string expressionString)
        {
            if (expressionString.Contains(" => "))
            {
                string left = expressionString.Substring(0, expressionString.IndexOf(" => "));
                string right = expressionString.Substring(expressionString.IndexOf(" => ") + 4);

                expressionString = string.Format("property => {0}", right.Replace(left + ".", "property."));
            }
            return expressionString;
        }
    }
}