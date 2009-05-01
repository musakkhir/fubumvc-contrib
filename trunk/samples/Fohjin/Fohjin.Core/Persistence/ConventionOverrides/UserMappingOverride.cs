using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;
using FluentNHibernate.Mapping;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Persistence.ConventionOverrides
{
    public class UserMappingOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMap<User> mapping)
        {
            mapping
                .IgnoreProperty(x => x.IsAuthenticated);

            mapping
                .HasMany(x => x.GetPosts())
                .Access
                .AsCamelCaseField(Prefix.Underscore)
                .AsBag();
        }
    }
}