using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Validation.Rules;

namespace FubuMVC.Validation.Results
{
    public class ValidationResults<TViewModel> : IValidationResults<TViewModel> where TViewModel : class
    {
        private readonly IDictionary<string, IList<IValidationRule<TViewModel>>> _invalidFields;
        private readonly ValidationRuleConvertor _validationRuleConvertor;

        public ValidationResults()
        {
            _invalidFields = new Dictionary<string, IList<IValidationRule<TViewModel>>>();
            _validationRuleConvertor = new ValidationRuleConvertor();
        }

        public void AddInvalidField(string propertyString, IValidationRule<TViewModel> validationRule)
        {
            if (_invalidFields.ContainsKey(propertyString))
                _invalidFields[propertyString].Add(validationRule);
            else
                _invalidFields[propertyString] = new List<IValidationRule<TViewModel>> { validationRule };
        }

        public IEnumerable<string> GetInvalidFields()
        {
            return _invalidFields.Keys.Distinct().AsEnumerable();
        }

        public IEnumerable<IValidationRule<TViewModel>> GetBrokenRulesFor(string propertyString)
        {
            return _invalidFields.ContainsKey(propertyString)
                       ? _invalidFields[propertyString].Distinct().AsEnumerable()
                       : new List<IValidationRule<TViewModel>>();
        }

        public bool IsValid()
        {
            return _invalidFields.Count() == 0;
        }

        public bool IsValid(string propertyString)
        {
            return !_invalidFields.ContainsKey(propertyString);
        }

        public void CloneFrom<TOtherViewModel>(IValidationResults<TOtherViewModel> validationResults) where TOtherViewModel : class
        {
            validationResults.GetInvalidFields()
                .Each(propertyString =>
                {
                    var rules = new List<IValidationRule<TViewModel>>();

                    validationResults.GetBrokenRulesFor(propertyString).Each(rule => 
                        rules.Add(_validationRuleConvertor.ConvertValidationRuleModelTo<TViewModel, TOtherViewModel>(rule)));

                    _invalidFields.Add(propertyString, rules);
                });
        }
    }
}