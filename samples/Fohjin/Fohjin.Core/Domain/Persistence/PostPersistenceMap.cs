using FluentNHibernate.Mapping;

namespace Fohjin.Core.Domain.Persistence
{
    public sealed class PostPersistenceMap : DomainEntityMap<Post>
    {
        public PostPersistenceMap()
        {
            MapEntity();
        }

        private void MapEntity() 
        {
            Map(u => u.Title);
            Map(u => u.Body).WithLengthOf(4001);
            Map(u => u.Slug).Unique();
            Map(u => u.Published);
            References(u => u.User).Cascade.SaveUpdate();
            HasManyToMany(u => u.GetTags()).Access.AsCamelCaseField(Prefix.Underscore).WithTableName("PostsToTags").Cascade.SaveUpdate();
            HasMany(u => u.GetComments()).Access.AsCamelCaseField(Prefix.Underscore).Cascade.All().Inverse();
        }
    }
}