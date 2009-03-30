using System.Collections.Generic;
using System.Linq;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;

namespace AltOxite.Core.Services
{
    public class PostService : IPostService
    {
        private IRepository _repository;

        public PostService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Post> GetPostsWithDrafts()
        {
            return _repository.Query<Post>().ToList().OrderBy(post => post.Published);
        }
    }
}