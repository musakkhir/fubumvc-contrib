using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;

namespace FubuMVC.Validation.Results
{
    public class ValidationResults : IValidationResults
    {
        private readonly IDictionary<string, IList<Type>> _invalidFields;

        public ValidationResults()
        {
            _invalidFields = new Dictionary<string, IList<Type>>();
        }

        public void AddInvalidField(string propertyString, Type validationRule)
        {
            if (_invalidFields.ContainsKey(propertyString))
                _invalidFields[propertyString].Add(validationRule);
            else
                _invalidFields[propertyString] = new List<Type> { validationRule };
        }

        public IEnumerable<string> GetInvalidFields()
        {
            return _invalidFields.Keys.Distinct().AsEnumerable();
        }

        public IEnumerable<Type> GetBrokenRulesFor(string propertyString)
        {
            return _invalidFields.ContainsKey(propertyString)
                       ? _invalidFields[propertyString].Distinct().AsEnumerable()
                       : new List<Type>();
        }

        public bool IsValid()
        {
            return _invalidFields.Count() == 0;
        }

        public bool IsValid(string propertyString)
        {
            return !_invalidFields.ContainsKey(propertyString);
        }

        public void CloneFrom(IValidationResults validationResults)
        {
            validationResults.GetInvalidFields()
                .Each(propertyString => _invalidFields.Add(propertyString, validationResults.GetBrokenRulesFor(propertyString).ToList()));
        }
    }
}