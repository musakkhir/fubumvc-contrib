using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Fohjin.Core.Persistence.Conventions
{
    public class ClassConvention : IClassConvention
    {
        public bool Accept(IClassMap target)
        {
            return true;
        }

        public void Apply(IClassMap target)
        {
            target.Not.LazyLoad();
        }
    }
}