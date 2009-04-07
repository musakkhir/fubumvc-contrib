using System.Collections.Generic;
using FubuMVC.Validation.Rules;

namespace FubuMVC.Validation.Results
{
    public interface IValidationResults<TViewModel> where TViewModel : class
    {
        void AddInvalidField(string propertyString, IValidationRule<TViewModel> validationRule);
        IEnumerable<string> GetInvalidFields();
        IEnumerable<IValidationRule<TViewModel>> GetBrokenRulesFor(string propertyString);
        bool IsValid();
        bool IsValid(string propertyString);
        void CloneFrom<TOtherViewModel>(IValidationResults<TOtherViewModel> validationResults) where TOtherViewModel : class;
    }
}