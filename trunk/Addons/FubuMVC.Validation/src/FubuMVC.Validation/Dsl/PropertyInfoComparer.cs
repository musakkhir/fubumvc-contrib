using System.Collections.Generic;
using System.Reflection;

namespace FubuMVC.Validation.Dsl
{
    public class PropertyInfoComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}