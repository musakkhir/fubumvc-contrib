using FubuMVC.Validation.Behaviors;
using FubuMVC.Validation.SemanticModel;
using StructureMap.Configuration.DSL;

namespace FubuMVC.Validation.StructureMap
{
    public class StructureMapConfigurer
    {
        private readonly ValidationConfiguration _validationConfiguration;

        public StructureMapConfigurer(ValidationConfiguration validationConfigurationOld)
        {
            _validationConfiguration = validationConfigurationOld;
        }

        public void ConfigureRegistry(Registry registry)
        {
            registry.ForRequestedType<IValidate>().TheDefault.IsThis(_validationConfiguration);
        }
    }
}