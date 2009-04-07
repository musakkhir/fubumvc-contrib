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
            var viewModelType = typeof (TViewModel);

            var inActualClass = viewModelType.GetProperties().Contains(property, new PropertyInfoComparer());
            var notInBaseClass = !viewModelType.BaseType.GetProperties().Contains(property, new PropertyInfoComparer());

            return inActualClass && notInBaseClass;
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
            var interfaces = type.GetInterfaces();
            var firstOrDefault = interfaces
                .Where(t => t.IsGenericType && 
                            t.GetGenericTypeDefinition() == typeof (ICanBeValidated<>))
                .FirstOrDefault();
            return firstOrDefault == null 
                    ? false 
                    : true;
        }
    }
}