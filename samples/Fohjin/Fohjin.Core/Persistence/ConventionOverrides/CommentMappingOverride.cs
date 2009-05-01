using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Persistence.ConventionOverrides
{
    public class CommentMappingOverride : IAutoMappingOverride<Comment>
    {
        public void Override(AutoMap<Comment> mapping)
        {
            mapping.References(c => c.Post).Not.Nullable().Cascade.All();
            mapping.References(c => c.User).Not.Nullable().Cascade.All();
        }
    }
}