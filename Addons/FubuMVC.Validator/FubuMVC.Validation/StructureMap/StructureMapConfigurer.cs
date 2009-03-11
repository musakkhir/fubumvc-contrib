using FubuMVC.Validation.DSL;
using StructureMap.Configuration.DSL;

namespace FubuMVC.Validation.StructureMap
{
    public class StructureMapConfigurer
    {
        private readonly ValidatorConfiguration _validatorConfiguration;

        public StructureMapConfigurer(ValidatorConfiguration validatorConfiguration)
        {
            _validatorConfiguration = validatorConfiguration;
        }

        public void ConfigureRegistry(Registry registry)
        {
            registry.ForRequestedType<ValidatorConfiguration>().TheDefault.IsThis(_validatorConfiguration);
        }
    }
}