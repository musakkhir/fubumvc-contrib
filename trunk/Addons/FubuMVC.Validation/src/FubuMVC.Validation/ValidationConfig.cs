using System;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.SemanticModel;
using FubuMVC.Validation.StructureMap;
using StructureMap.Configuration.DSL;

namespace FubuMVC.Validation
{
    public class ValidationConfig : Registry
    {
        public static Action<ValidationDsl> Configure = x =>
        {
            throw new NotImplementedException(@"There are no validation rules configured");
        };

        public ValidationConfig()
        {
            var validatorConfiguration = new ValidationConfiguration();
            var validationDSL = new ValidationDsl(validatorConfiguration);
            Configure(validationDSL);

            new StructureMapConfigurer(validatorConfiguration).ConfigureRegistry(this);
        }
    }
}