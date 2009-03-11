using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FubuMVC.Validation.Tests.ViewModels
{
    public class ValidationTestSUT : ICanBeValidated
    {
        public string Email { get; set; }
        public string EmailOptional { get; set; }
        public string Url { get; set; }
        public string UrlOptional { get; set; }
        public string AnyString { get; set; }
        public string AnyStringOptional { get; set; }

        private readonly IDictionary<PropertyInfo, IList<Type>> _invalidFields = new Dictionary<PropertyInfo, IList<Type>>();
        public void AddInvalidField(PropertyInfo propertyInfo, IValidationRule validationRule)
        {
            if (_invalidFields.ContainsKey(propertyInfo))
                _invalidFields[propertyInfo].Add(validationRule.GetType());
            else
                _invalidFields[propertyInfo] = new List<Type> { validationRule.GetType() };
        }
        public IEnumerable<PropertyInfo> GetInvalidFields()
        {
            return _invalidFields.Keys.Distinct().AsEnumerable();
        }
        public IEnumerable<Type> GetBrokenRulesFor(PropertyInfo propertyInfo)
        {
            return _invalidFields.ContainsKey(propertyInfo)
                       ? _invalidFields[propertyInfo].Distinct().AsEnumerable()
                       : new List<Type>();
        }
    }

    public class ValidationTestSUT1 : ValidationTestSUT { }
    public class ValidationTestSUT2 : ValidationTestSUT { }
    public class ValidationTestSUT3 : ValidationTestSUT { }
}