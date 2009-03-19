using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Core;
using FubuMVC.Validation.Behaviors;
using FubuMVC.Validation.Results;
using FubuMVC.Validation.Rules;

namespace FubuMVC.Validation.SemanticModel
{
    public class ValidationConfiguration : IValidate
    {
        private readonly Dictionary<Type, object> _discoveredTypes;
        private readonly List<DefaultPropertyConvention> _defaultPropertyConventions;

        public ValidationConfiguration()
        {
            _discoveredTypes = new Dictionary<Type, object>();
            _defaultPropertyConventions = new List<DefaultPropertyConvention>();
        }

        public void Validate<TViewModel>(TViewModel viewModel) where TViewModel : ICanBeValidated
        {
            GetRulesFor(viewModel).Each(rule =>
            {
                if (!rule.IsValid(viewModel))
                    viewModel.ValidationResults.AddInvalidField(rule.PropertyFilter, rule.GetType());
            });
        }

        public void AddDefaultPropertyConvention(DefaultPropertyConvention defaultPropertyConvention)
        {
            var propertyConvention = _defaultPropertyConventions
                .Where(convention => convention.ToString() == defaultPropertyConvention.ToString())
                .FirstOrDefault();

            if (propertyConvention == null)
                _defaultPropertyConventions.Add(defaultPropertyConvention);
        }

        public IEnumerable<DefaultPropertyConvention> GetDefaultPropertyConventions()
        {
            return _defaultPropertyConventions.AsEnumerable();
        }

        public void AddDiscoveredType(Type viewModelType)
        {
            var method = GetType().GetMethod("AddDiscoveredType", new Type[] { });
            if (method.IsGenericMethod)
                method.MakeGenericMethod(new[] { viewModelType }).Invoke(this, new object[] { });
        }

        public void AddDiscoveredType<TViewModel>() where TViewModel : ICanBeValidated
        {
            var discoveredType = typeof (TViewModel);

            if (_discoveredTypes.ContainsKey(discoveredType)) return;

            var rules = new List<IValidationRule<TViewModel>>();
            _discoveredTypes.Add(discoveredType, rules);

            discoveredType.GetProperties().Each(property => _defaultPropertyConventions.Each(convention =>
            {
                if (convention.Match.Compile().Invoke(property))
                {
                    convention.GetValidationRules()
                        .Each(ruleType => ValidationRuleBuilder<TViewModel>(discoveredType, ruleType, property, rules.Add));
                }
            }));
        }

        private static void ValidationRuleBuilder<TViewModel>(Type discoveredType, Type ruleType, PropertyInfo property, Action<IValidationRule<TViewModel>> addRuleToRules) where TViewModel : ICanBeValidated
        {
            ParameterExpression arg = Expression.Parameter(discoveredType, "x");
            Expression expr = arg;
            expr = Expression.Property(expr, property);
            LambdaExpression lambdaExpression = Expression.Lambda(expr, arg);

            var genericType = ruleType.MakeGenericType(new[] { discoveredType });

            var constructor = genericType.GetConstructor(new [] {lambdaExpression.GetType()});

            if (constructor == null) return;

            IValidationRule<TViewModel> instance = (IValidationRule<TViewModel>)constructor.Invoke(new object[] { lambdaExpression });

            if (instance != null)
                addRuleToRules(instance);
        }

        public IEnumerable<Type> GetDiscoveredTypes()
        {
            return _discoveredTypes.Keys.AsEnumerable();
        }

        public IEnumerable<IValidationRule<TViewModel>> GetRulesFor<TViewModel>(TViewModel viewModel) where TViewModel : ICanBeValidated
        {
            var validatorRules = new List<IValidationRule<TViewModel>>();

            if (!_discoveredTypes.ContainsKey(viewModel.GetType()))
                return validatorRules.AsEnumerable();

            return _discoveredTypes[viewModel.GetType()] as IList<IValidationRule<TViewModel>>;
        }

        public void AddRuleFor<TViewModel>(Expression<Func<PropertyInfo, bool>> propertyFilter, Type validationRuleType) where TViewModel : ICanBeValidated
        {
            var discoveredType = typeof(TViewModel);
            if (!_discoveredTypes.ContainsKey(discoveredType)) return;

            var rules = _discoveredTypes[discoveredType] as IList<IValidationRule<TViewModel>>;
            var genericValidationRuleType = validationRuleType.MakeGenericType(discoveredType);

            if (rules == null || rules.Where(rule => 
                    rule.GetType() == genericValidationRuleType).FirstOrDefault() != null) return;

            discoveredType.GetProperties().Each(property =>
            {
                if (propertyFilter.Compile().Invoke(property))
                        ValidationRuleBuilder<TViewModel>(discoveredType, validationRuleType, property, rules.Add);
            });
        }

        public void RemoveRuleFrom<TViewModel, TValidationRule>() 
            where TViewModel : ICanBeValidated 
            where TValidationRule : IValidationRule<TViewModel>
        {
            Type discoveredType = typeof(TViewModel);
            if (!_discoveredTypes.ContainsKey(discoveredType)) return;

            var validationRules = _discoveredTypes[discoveredType] as IList<IValidationRule<TViewModel>>;
            if (validationRules == null || validationRules.Count() == 0) return;

            Type validationRuleType = typeof(TValidationRule).GetGenericTypeDefinition().MakeGenericType(discoveredType);

            var selectedValidationRules = validationRules.Where(rule => rule.GetType() == validationRuleType).ToList();
            var rules = selectedValidationRules
                .Where(rule => rule.GetType() == validationRuleType)
                .ToList();
            rules.Each(rule => validationRules.Remove(rule));
        }

        public void RemoveAllRulesFor<TViewModel>() where TViewModel : ICanBeValidated
        {
            if (!_discoveredTypes.ContainsKey(typeof(TViewModel))) return;

            var validationRules = _discoveredTypes[typeof(TViewModel)] as IList<IValidationRule<TViewModel>>;

            if (validationRules == null) return;

            validationRules.Clear();
        }
    }
}