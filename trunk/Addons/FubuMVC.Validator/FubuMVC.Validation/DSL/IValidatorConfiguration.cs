using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuMVC.Validation.DSL
{
    public interface IValidatorConfiguration 
    {
        IEnumerable<PropertyConvention> GetDefaultConventions();
        PropertyConvention AddDefaultPropertyConvention(Expression<Func<PropertyInfo, bool>> property);
        void AddDiscoveredType(Type type);
        IEnumerable<ViewModelPropertyRules> GetDiscoveredTypes();
        void Validate(ICanBeValidated viewModel);
    }
}