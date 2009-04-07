using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuMVC.Validation.Rules;

namespace FubuMVC.Validation.Results
{
    public class ValidationRuleConvertor
    {
        public IValidationRule<TViewModel> ConvertValidationRuleModelTo<TViewModel, TOtherViewModel>(IValidationRule<TOtherViewModel> rule)
            where TViewModel : class
            where TOtherViewModel : class
        {
            if (typeof(TViewModel) == typeof(TOtherViewModel))
                return rule as IValidationRule<TViewModel>;

            Type genericType = rule.GetType();
            if (genericType.IsGenericType)
            {
                genericType = genericType.GetGenericTypeDefinition();
                genericType = genericType.MakeGenericType(new[] {typeof (TViewModel)});
            }

            IEnumerable<object> convertedExpressions = rule.ConstructorArguments.Select(o => ConvertExpressionTo<TViewModel>(o)).ToList();

            var types = convertedExpressions.Select(o => o.GetType()).ToArray();

            var constructor = genericType.GetConstructor(types);
            return (IValidationRule<TViewModel>)constructor.Invoke(convertedExpressions.ToArray());
        }

        private static object ConvertExpressionTo<TViewModel>(object sourceExpression)
            where TViewModel : class
        {
            LambdaExpression lambdaExpression = (LambdaExpression)sourceExpression;
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TViewModel), "x");
            return Expression.Lambda(lambdaExpression.Body, parameterExpression);
        }
    }
}