using System;
using FubuMVC.Validation.DSL;
using FubuMVC.Validation.StructureMap;
using StructureMap.Configuration.DSL;

namespace FubuMVC.Validation
{
    public class ValidationConfig : Registry
    {
        public static Action<ValidationDSL> Configure = x =>
        {
            throw new NotImplementedException(@"There are no validation rules configured");
        };

        public ValidationConfig()
        {
            var validatorConfiguration = new ValidatorConfiguration();
            var validationDSL = new ValidationDSL(validatorConfiguration);
            Configure(validationDSL);

            new StructureMapConfigurer(validatorConfiguration).ConfigureRegistry(this);
        }
    }
}