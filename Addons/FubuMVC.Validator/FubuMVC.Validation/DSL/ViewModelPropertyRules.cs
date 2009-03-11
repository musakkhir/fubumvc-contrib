using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Core;

namespace FubuMVC.Validation.DSL
{
    public class ViewModelPropertyRules
    {
        private readonly List<PropertyConvention> _defaultConventions;
        private readonly UglyStringComparer _uglyStringComparer;

        public ViewModelPropertyRules()
        {
            _defaultConventions = new List<PropertyConvention>();
            _uglyStringComparer = new UglyStringComparer();
        }

        public Type ViewModel { get; set; }

        public void AddConvention(PropertyConvention propertyConvention)
        {
            _defaultConventions.Add(propertyConvention);
        }
        public IEnumerable<PropertyConvention> GetConventions()
        {
            return _defaultConventions.AsEnumerable();
        }

        public void Validate(ICanBeValidated viewModel)
        {
            viewModel.GetType().GetProperties().Each(property => _defaultConventions.Each(convention =>
            {
                if (convention.Property.Compile().Invoke(property))
                    convention.GetRules().Each(rule =>
                    {
                        if (!rule.Validate(property.GetValue(viewModel, new object[] {})))
                            viewModel.AddInvalidField(property, rule);
                    });
            }));
        }

        public PropertyConvention ForConvention(Expression<Func<PropertyInfo, bool>> propertySelector)
        {
            PropertyConvention propertyConvention = _defaultConventions
                .Where(p => _uglyStringComparer.Compare(propertySelector, p.Property))
                .FirstOrDefault();

            if (propertyConvention == null)
            {
                propertyConvention = new PropertyConvention(propertySelector);
                _defaultConventions.Add(propertyConvention);
            }
            return propertyConvention;
        }

        public void RemoveConvention(Expression<Func<PropertyInfo, bool>> propertySelector)
        {
            PropertyConvention propertyConvention = _defaultConventions
                .Where(p => _uglyStringComparer.Compare(propertySelector, p.Property))
                .FirstOrDefault();
            if (propertyConvention != null)
            {
                _defaultConventions.Remove(propertyConvention);
            }
        }
    }
}