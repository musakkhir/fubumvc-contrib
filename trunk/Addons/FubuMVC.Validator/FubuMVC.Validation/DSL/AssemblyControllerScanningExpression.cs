using System;

namespace FubuMVC.Validation.DSL
{
    public class AssemblyControllerScanningExpression
    {
        private readonly IValidatorConfiguration _validatorConfiguration;

        public AssemblyControllerScanningExpression(IValidatorConfiguration validatorConfiguration)
        {
            _validatorConfiguration = validatorConfiguration;
        }

        public void ContainingType<TTypeInAssembly>(Action<ViewModelTypeScanningExpression> typeScannerAction)
        {
            var assembly = typeof(TTypeInAssembly).Assembly;
            var expression = new ViewModelTypeScanningExpression(_validatorConfiguration, assembly);
            typeScannerAction(expression);
        }
    }
}