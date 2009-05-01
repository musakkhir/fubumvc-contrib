using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;

namespace Fohjin.Core.Persistence.Conventions
{
    public class IdentityColumnConvention : IIdConvention  
    {
        public bool Accept(IIdentityPart id)
        {
            return true;
        }

        public void Apply(IIdentityPart id)
        {
            id.GeneratedBy.GuidComb();
        } 
    }
}