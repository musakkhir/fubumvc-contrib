using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;
using FluentNHibernate.Mapping;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Persistence.ConventionOverrides
{
    public class PostMappingOverride : IAutoMappingOverride<Post>
    {
        public void Override(AutoMap<Post> mapping)
        {
            mapping.Map(u => u.Slug)
                .Unique();

            mapping
                .HasManyToMany(u => u.GetTags())
                .Access
                .AsCamelCaseField(Prefix.Underscore)
                .WithTableName("PostsToTags");

            mapping
                .HasMany(u => u.GetComments())
                .Access
                .AsCamelCaseField(Prefix.Underscore);
        }
    }
}