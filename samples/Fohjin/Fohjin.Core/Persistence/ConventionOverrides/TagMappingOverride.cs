using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Persistence.ConventionOverrides
{
    public class TagMappingOverride : IAutoMappingOverride<Tag>
    {
        public void Override(AutoMap<Tag> mapping)
        {
        }
    }
}