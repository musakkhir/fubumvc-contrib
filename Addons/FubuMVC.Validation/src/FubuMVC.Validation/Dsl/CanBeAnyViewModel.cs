using System;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Dsl
{
    public class CanBeAnyViewModel : ICanBeValidated<CanBeAnyViewModel>
    {
        public IValidationResults<CanBeAnyViewModel> ValidationResults
        {
            get { throw new NotImplementedException("This is a placeholder only to be used in the validation configuration!"); }
        }
    }
}