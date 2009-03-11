using System;
using System.Reflection;
using FubuMVC.Core;

namespace FubuMVC.Validation.DSL
{
    public class ViewModelTypeScanningExpression
    {
        private readonly IValidatorConfiguration _validatorConfiguration;
        private readonly Assembly _assembly;

        public ViewModelTypeScanningExpression(IValidatorConfiguration validatorConfiguration, Assembly assembly)
        {
            _validatorConfiguration = validatorConfiguration;
            _assembly = assembly;
        }

        public void Where(Func<Type, bool> evalTypeFunc)
        {
            _assembly.GetExportedTypes().Each(type =>
            {
                if (!typeof(ICanBeValidated).IsAssignableFrom(type)) return;

                if (type.IsAbstract) return;

                if (type.IsValueType) return;

                if (evalTypeFunc(type)) _validatorConfiguration.AddDiscoveredType(type);
            });
        }
    }
}