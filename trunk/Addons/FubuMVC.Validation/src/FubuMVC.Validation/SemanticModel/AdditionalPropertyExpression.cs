using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.SemanticModel
{
    public class AdditionalPropertyExpression
    {
        private readonly List<Property> _properties;

        public AdditionalPropertyExpression()
        {
            _properties = new List<Property>();
        }

        public void NeedsAdditionalProperty(Expression<Func<PropertyInfo, bool>> filter)
        {
            var property = new Property(filter);
            if (!_properties.Contains(property, new PropertyComparer()))
                _properties.Add(property);
        }

        public IEnumerable<Property> GetProperties()
        {
            return _properties.AsEnumerable();
        }
    }
}