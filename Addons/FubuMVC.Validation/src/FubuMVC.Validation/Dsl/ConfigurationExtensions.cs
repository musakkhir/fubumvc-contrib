using System;
using System.Linq;
using System.Reflection;
using FubuMVC.Validation.Results;

namespace FubuMVC.Validation.Dsl
{
    public static class ConfigurationExtensions
    {
        public static bool IsDeclaredIn<TViewModel>(this PropertyInfo property)
        {
            return property.DeclaringType == typeof(TViewModel);
        }

        public static bool ImplementsICanBeValidated(this Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (ICanBeValidated<>));
        }
    }
}