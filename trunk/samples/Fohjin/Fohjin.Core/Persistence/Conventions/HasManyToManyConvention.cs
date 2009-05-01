using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Fohjin.Core.Persistence.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention
    {
        public bool Accept(IManyToManyPart target)
        {
            return true;
        }

        public void Apply(IManyToManyPart target)
        {
            target.Cascade.SaveUpdate();
        }
    }
}