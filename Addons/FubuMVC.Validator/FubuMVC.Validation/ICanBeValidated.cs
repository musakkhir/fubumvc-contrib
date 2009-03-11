using System;
using System.Collections.Generic;
using System.Reflection;

namespace FubuMVC.Validation
{
    public interface ICanBeValidated
    {
        void AddInvalidField(PropertyInfo propertyInfo, IValidationRule validationRule);
        IEnumerable<PropertyInfo> GetInvalidFields();
        IEnumerable<Type> GetBrokenRulesFor(PropertyInfo propertyInfo);
    }
}