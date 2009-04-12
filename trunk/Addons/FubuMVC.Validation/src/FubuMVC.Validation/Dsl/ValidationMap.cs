using System;
using System.Linq.Expressions;

namespace FubuMVC.Validation.Dsl
{
    public class ValidationMap<TObject> where TObject : class
    {
        public void Property(Expression<Func<TObject, object>> propertySelector, Expression<Action<RuleExpression>> rules)
        {
            //new RuleExpression()
            throw new NotImplementedException();
        }
    }
}