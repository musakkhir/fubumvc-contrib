using System;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ValidationRuleSetup
    {
        public ValidationRuleSetup(Type validationRuleType, AdditionalProperties additionalProperties)
        {
            AdditionalProperties = additionalProperties;
            ValidationRuleType = validationRuleType;
        }

        public Type ValidationRuleType { get; private set; }
        public AdditionalProperties AdditionalProperties { get; private set; }
    }
}