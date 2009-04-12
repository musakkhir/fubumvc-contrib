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

        public static bool NameStartsWith(this PropertyInfo property, string filter)
        {
            return property.Name.StartsWith(filter);
        }

        public static bool NameEndsWith(this PropertyInfo property, string filter)
        {
            return property.Name.EndsWith(filter);
        }

        public static bool NameContains(this PropertyInfo property, string filter)
        {
            return property.Name.Contains(filter);
        }

        public static bool ImplementsICanBeValidated(this Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (ICanBeValidated<>));
        }
    }
}