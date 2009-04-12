using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Core;
using FubuMVC.Validation.Rules;

namespace FubuMVC.Validation.SemanticModel
{
    public class ValidationRuleBuilder
    {
        public void Build<TViewModel>(Type discoveredType, Type ruleType, PropertyInfo property, IList<PropertyInfo> aditionalProperties, Action<IValidationRule<TViewModel>> addRuleToRules) where TViewModel : class
        {
            LambdaExpression lambdaExpression = LambdaExpressionCreator(discoveredType, property);

            var constructorParameters = new List<object>();
            var constructorParametersTypes = new List<Type>();

            constructorParameters.Add(LambdaExpressionCreator(discoveredType, property));
            constructorParametersTypes.Add(lambdaExpression.GetType());

            aditionalProperties.Each(p =>
            {
                LambdaExpression expression = LambdaExpressionCreator(discoveredType, p);
                constructorParameters.Add(expression);
                constructorParametersTypes.Add(expression.GetType());
            });

            var genericType = ruleType.MakeGenericType(new[] { discoveredType });

            var constructor = genericType.GetConstructor(constructorParametersTypes.ToArray());

            if (constructor == null) return;

            IValidationRule<TViewModel> instance = (IValidationRule<TViewModel>)constructor.Invoke(constructorParameters.ToArray());

            if (instance != null)
                addRuleToRules(instance);
        }

        private static LambdaExpression LambdaExpressionCreator(Type discoveredType, PropertyInfo property)
        {
            ParameterExpression parameterExpression = Expression.Parameter(discoveredType, "x");
            Expression expression = parameterExpression;
            expression = Expression.Property(expression, property);
            return Expression.Lambda(expression, parameterExpression);
        }

        //private static Expression GetPropertyExpression(Type mockType, PropertyInfo property)
        //{
        //    ParameterExpression expression = Expression.Parameter(mockType, "m");
        //    return Expression.Lambda(Expression.MakeMemberAccess(expression, property), new[] { expression });
        //}
    }
}