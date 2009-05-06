using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FubuMVC.Framework.NHibernate.Config.AutoMapConventions
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