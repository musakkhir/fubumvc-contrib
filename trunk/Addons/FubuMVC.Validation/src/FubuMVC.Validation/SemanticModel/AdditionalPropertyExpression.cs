using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.SemanticModel
{
    public class AdditionalPropertyExpression
    {
        private readonly AdditionalProperties _additionalProperties;

        public AdditionalPropertyExpression(AdditionalProperties additionalProperties)
        {
            _additionalProperties = additionalProperties;
        }

        public void NeedsAdditionalProperty(Expression<Func<PropertyInfo, bool>> filter)
        {
            var property = new Property(filter);
            if (!_additionalProperties.Contains(property, new PropertyComparer()))
                _additionalProperties.AddProperty(property);
        }
    }
}