using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FubuMVC.Framework.NHibernate.Config.AutoMapConventions
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