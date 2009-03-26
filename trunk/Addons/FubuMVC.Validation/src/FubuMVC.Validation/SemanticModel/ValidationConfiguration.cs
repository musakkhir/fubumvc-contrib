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
        private readonly ValidationRuleBuilder _validationRuleBuilder;

        public ValidationConfiguration()
        {
            _discoveredTypes = new Dictionary<Type, object>();
            _defaultPropertyConventions = new List<DefaultPropertyConvention>();
            _validationRuleBuilder = new ValidationRuleBuilder();
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
            if (!_defaultPropertyConventions.Contains(defaultPropertyConvention, new DefaultPropertyConventionComparer()))
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
                if (convention.Property.Match.Compile().Invoke(property))
                {
                    convention.GetValidationRules()
                        .Each(ruleType =>
                        {
                            var propertyInfos = new List<PropertyInfo>();
                            convention.GetAdditionalPropertiesForRule(ruleType).Each(additionalProperty => discoveredType.GetProperties().Each(p =>
                            {
                                if (!additionalProperty.Match.Compile().Invoke(p)) return;

                                propertyInfos.Add(p);
                                return;
                            }));

                            _validationRuleBuilder.Build<TViewModel>(discoveredType, ruleType, property, propertyInfos, rules.Add);
                        });
                }
            }));
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
                {
                    var propertyInfos = new List<PropertyInfo>();
                    //convention.GetAdditionalPropertiesForRule(ruleType).Each(additionalProperty => discoveredType.GetProperties().Each(p =>
                    //{
                    //    if (!additionalProperty.Match.Compile().Invoke(p)) return;

                    //    propertyInfos.Add(p);
                    //    return;
                    //}));

                    _validationRuleBuilder.Build<TViewModel>(discoveredType, validationRuleType, property, propertyInfos, rules.Add);
                }
            });
        }

        public void RemoveRuleFrom<TViewModel, TValidationRule>() where TViewModel : ICanBeValidated where TValidationRule : IValidationRule<TViewModel>
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