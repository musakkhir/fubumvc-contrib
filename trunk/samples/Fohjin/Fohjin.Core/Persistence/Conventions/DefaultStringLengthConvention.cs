using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Fohjin.Core.Persistence.Conventions
{
    public class DefaultStringLengthConvention : IPropertyConvention
    {
        public bool Accept(IProperty target)
        {
            return target.PropertyType == typeof(string) &&
                  !target.HasAttribute("length");  
        }

        public void Apply(IProperty target)
        {
            target.WithLengthOf(250);
        }
    }
}