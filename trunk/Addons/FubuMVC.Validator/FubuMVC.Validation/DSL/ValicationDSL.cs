using System;
using System.Linq;

namespace FubuMVC.Validation.DSL
{
    public class ValidationDSL
    {
        private readonly IValidatorConfiguration _validatorConfiguration;

        public ValidationDSL(IValidatorConfiguration validatorConfiguration)
        {
            _validatorConfiguration = validatorConfiguration;
        }

        public ByDefaultDslChain ByDefault
        {
            get { return new ByDefaultDslChain(_validatorConfiguration); }
        }

        public AssemblyControllerScanningExpression AddViewModelsFromAssembly
        {
            get { return new AssemblyControllerScanningExpression(_validatorConfiguration); }
        }

        public void OverrideConfigFor<TViewModel>(Action<ViewModelPropertyRules> action) where TViewModel : ICanBeValidated
        {
            ViewModelPropertyRules viewModelPropertyRules = _validatorConfiguration.GetDiscoveredTypes().ToList()
                .Where(d => d.ViewModel == typeof(TViewModel)).FirstOrDefault() 
                ?? new ViewModelPropertyRules { ViewModel = typeof(TViewModel) };

            action(viewModelPropertyRules);
        }
    }
}