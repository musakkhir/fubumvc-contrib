using System;
using System.Reflection;
using System.Linq.Expressions;

namespace FubuMVC.Validation.DSL
{
    public class UglyStringComparer
    {
        public bool Compare(Expression<Func<PropertyInfo, bool>> expressionInConvention, Expression<Func<PropertyInfo, bool>> searchExpression)
        {
            return ParseExpression(expressionInConvention) == ParseExpression(searchExpression);
        }

        public string ParseExpression(Expression<Func<PropertyInfo, bool>> expression)
        {
            var expressionString = expression.ToString();
            if (expressionString.Contains(" => "))
            {
                string left = expressionString.Substring(0, expressionString.IndexOf(" => "));
                string right = expressionString.Substring(expressionString.IndexOf(" => ") + 4);

                expressionString = right.Replace(left + ".", "property.");
            }
            return expressionString;
        }
    }
}