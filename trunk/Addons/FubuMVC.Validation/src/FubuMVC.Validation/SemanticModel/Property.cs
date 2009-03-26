using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.SemanticModel
{
    public class Property
    {
        private readonly string _expressionString;

        public Property(Expression<Func<PropertyInfo, bool>> filter)
        {
            Match = filter;
            _expressionString = new UglyExpressionConvertor().ToString(Match);
        }

        public Expression<Func<PropertyInfo, bool>> Match { get; private set; }

        public override string ToString()
        {
            return _expressionString;
        }
    }
}