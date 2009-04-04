using System.Linq;
using System.Reflection;

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
    }
}