using System.Collections.Generic;

namespace FubuMVC.Validation.SemanticModel
{
    public class PropertyComparer : IEqualityComparer<Property>
    {
        public bool Equals(Property x, Property y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(Property obj)
        {
            return obj.GetHashCode();
        }
    }
}