using System;
using System.Linq.Expressions;
using FubuMVC.Core.Util;

namespace FubuMVC.Validation.SemanticModel
{
    public class ExtendedAdditionalPropertyExpression<TViewModel> where TViewModel : class
    {
        private readonly AdditionalProperties _additionalProperties;

        public ExtendedAdditionalPropertyExpression(AdditionalProperties additionalProperties)
        {
            _additionalProperties = additionalProperties;
        }

        public void NeedsAdditionalProperty(Expression<Func<TViewModel, object>> property)
        {
            _additionalProperties.AddProperty(new Property(x => x == ReflectionHelper.GetProperty(property)));
        }
    }
}