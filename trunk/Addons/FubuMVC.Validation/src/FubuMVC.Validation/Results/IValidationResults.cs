using System;
using System.Collections.Generic;

namespace FubuMVC.Validation.Results
{
    public interface IValidationResults
    {
        void AddInvalidField(string propertyString, Type validationRule);
        IEnumerable<string> GetInvalidFields();
        IEnumerable<Type> GetBrokenRulesFor(string propertyString);
    }
}