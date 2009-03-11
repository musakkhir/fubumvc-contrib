using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Core;

namespace FubuMVC.Validation.DSL
{
    public class ValidatorConfiguration : IValidatorConfiguration
    {
        private readonly List<PropertyConvention> _defaultConventions;
        private readonly List<ViewModelPropertyRules> _viewModelPropertyRules;
        private readonly UglyStringComparer _uglyStringComparer;
 
        public ValidatorConfiguration()
        {
            _defaultConventions = new List<PropertyConvention>();
            _viewModelPropertyRules = new List<ViewModelPropertyRules>();
            _uglyStringComparer = new UglyStringComparer();
        }

        public IEnumerable<PropertyConvention> GetDefaultConventions()
        {
            return _defaultConventions.AsEnumerable();
        }

        public PropertyConvention AddDefaultPropertyConvention(Expression<Func<PropertyInfo, bool>> property)
        {
            var propertyConvention = _defaultConventions
                .Where(p => _uglyStringComparer.Compare(property, p.Property))
                .FirstOrDefault();

            if (propertyConvention == null)
            {
                propertyConvention = new PropertyConvention(property);
                _defaultConventions.Add(propertyConvention);
            }
            return propertyConvention;
        }

        public void AddDiscoveredType(Type type)
        {
            var viewModelPropertyRules = new ViewModelPropertyRules {ViewModel = type};
            _defaultConventions.Each(viewModelPropertyRules.AddConvention);

            _viewModelPropertyRules.Add(viewModelPropertyRules);
        }

        public IEnumerable<ViewModelPropertyRules> GetDiscoveredTypes()
        {
            return _viewModelPropertyRules.AsEnumerable();
        }

        public void Validate(ICanBeValidated viewModel)
        {
            var viewModelPropertyRules = _viewModelPropertyRules
                .Where(view => view.ViewModel == viewModel.GetType())
                .FirstOrDefault();

            if (viewModelPropertyRules == null) return;

            viewModelPropertyRules.Validate(viewModel);
        }
    }
}