using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Fohjin.Core.Persistence.Conventions
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