using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;

namespace Fohjin.Core.Persistence.Conventions
{
    public class ForeignKeyConvention : IHasManyConvention
    {
        public bool Accept(IOneToManyPart target)
        {
            return true;
        }

        public void Apply(IOneToManyPart target)
        {
            target.KeyColumnNames.Add(target.EntityType.Name + "_Id");
        }
    }
}