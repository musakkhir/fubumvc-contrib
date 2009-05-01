using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;

namespace Fohjin.Core.Persistence.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public bool Accept(IOneToManyPart target)
        {
            return true;
        }

        public void Apply(IOneToManyPart target)
        {
            target.Cascade.SaveUpdate().Inverse();
        }
    }
}