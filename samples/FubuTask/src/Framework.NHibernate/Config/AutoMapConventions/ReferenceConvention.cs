using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FubuMVC.Framework.NHibernate.Config.AutoMapConventions
{
    public class ReferenceConvention : IReferenceConvention
    {
        public bool Accept(IManyToOnePart target)
        {
            return true;
        }

        public void Apply(IManyToOnePart target)
        {
            target.Cascade.SaveUpdate();
        }
    }
}