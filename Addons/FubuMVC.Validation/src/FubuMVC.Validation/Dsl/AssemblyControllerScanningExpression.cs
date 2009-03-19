using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class AssemblyControllerScanningExpression
    {
        private readonly ValidationConfiguration _validationConfiguration;

        public AssemblyControllerScanningExpression(ValidationConfiguration validationConfiguration)
        {
            _validationConfiguration = validationConfiguration;
        }

        public ViewModelTypeScanningExpression<TTypeInAssembly> ContainingType<TTypeInAssembly>()
        {
            return new ViewModelTypeScanningExpression<TTypeInAssembly>(_validationConfiguration);
        }
    }
}