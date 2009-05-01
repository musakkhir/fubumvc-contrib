using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;

namespace Fohjin.Core.Persistence.Conventions
{
    public class DefaultBodyStringLengthConvention : IPropertyConvention
    {
        public bool Accept(IProperty target)
        {
            return target.PropertyType == typeof(string) &&
                   target.Property.Name == "Body";  
        }

        public void Apply(IProperty target)
        {
            target.WithLengthOf(4001);
        }
    }
}