using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ValidationDsl
    {
        private readonly ValidationConfiguration _validationConfiguration;

        public ValidationDsl(ValidationConfiguration validationConfiguration)
        {
            _validationConfiguration = validationConfiguration;
        }

        public ByDefaultDslChain ByDefault
        {
            get { return new ByDefaultDslChain(_validationConfiguration); }
        }

        public AssemblyControllerScanningExpression AddViewModelsFromAssembly
        {
            get { return new AssemblyControllerScanningExpression(_validationConfiguration); }
        }

        public PropertiesMatchingExpression<TViewModel> OverrideConfigFor<TViewModel>() where TViewModel : class
        {
            return new PropertiesMatchingExpression<TViewModel>(_validationConfiguration);
        }
    }
}