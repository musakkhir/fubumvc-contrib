using System;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Dsl
{
    public class CanBeAnyViewModel : ICanBeValidated
    {
        public IValidationResults ValidationResults
        {
            get { throw new NotImplementedException("This is a placeholder only to be used in the validation configuration!"); }
        }
    }
}