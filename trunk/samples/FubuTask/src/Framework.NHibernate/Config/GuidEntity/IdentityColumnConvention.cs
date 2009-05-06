using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FubuMVC.Framework.NHibernate.Config.GuidEntity
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