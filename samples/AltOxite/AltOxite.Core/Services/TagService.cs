using System;
using System.Linq;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;

namespace AltOxite.Core.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository _repository;

        public TagService(IRepository repository)
        {
            _repository = repository;
        }

        public Tag CreateOrGetTagForItem(string tagForItem)
        {
            var tags = from tag in _repository.Query<Tag>()
                       select tag;

            foreach (var tag in tags)
                if (tag.Name == tagForItem.ToLowerInvariant()) return tag;

            return new Tag { CreatedDate = DateTime.Today, Name = tagForItem.ToLower() };
        }
    }
}