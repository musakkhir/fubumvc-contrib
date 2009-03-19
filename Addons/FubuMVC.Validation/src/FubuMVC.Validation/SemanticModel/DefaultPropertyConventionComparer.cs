using System.Collections.Generic;

namespace FubuMVC.Validation.SemanticModel
{
    public class DefaultPropertyConventionComparer : IEqualityComparer<DefaultPropertyConvention>
    {
        public bool Equals(DefaultPropertyConvention x, DefaultPropertyConvention y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(DefaultPropertyConvention obj)
        {
            return obj.GetHashCode();
        }
    }
}