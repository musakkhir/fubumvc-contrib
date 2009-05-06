using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FubuMVC.Framework.NHibernate.Config.AutoMapConventions
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