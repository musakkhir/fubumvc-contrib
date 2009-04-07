using System;
using System.Reflection;
using FubuMVC.Core;
using FubuMVC.Validation.SemanticModel;

namespace FubuMVC.Validation.Dsl
{
    public class ViewModelTypeScanningExpression<TTypeInAssembly> 
    {
        private readonly Assembly _assembly;
        private readonly ValidationConfiguration _validationConfiguration;

        public ViewModelTypeScanningExpression(ValidationConfiguration validationConfiguration)
        {
            _validationConfiguration = validationConfiguration;
            _assembly = typeof(TTypeInAssembly).Assembly;
        }

        public void Where(Func<Type, bool> evalTypeFunc)
        {
            _assembly.GetExportedTypes().Each(type =>
            {
                if (type.IsAbstract) return;

                if (type.IsValueType) return;

                if (evalTypeFunc(type)) _validationConfiguration.DiscoveredTypes.AddDiscoveredType(type);
            });
        }
    }
}