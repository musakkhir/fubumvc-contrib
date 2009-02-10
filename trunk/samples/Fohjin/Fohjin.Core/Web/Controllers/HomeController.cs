using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Web.DisplayModels;

namespace Fohjin.Core.Web.Controllers
{
    public class HomeController
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IndexViewModel Index(IndexSetupViewModel inModel)
        {
            var posts = _repository.Query<Post>().OrderByDescending(p => p.Published);
            var recentPosts = _repository.Query<Post>().OrderByDescending(p => p.Published).Take(10).ToList();

            return new IndexViewModel {Posts = posts.ToList().Select(p => new PostDisplay(p))};
        }
    }

    public class IndexSetupViewModel : ViewModel
    {
    }

    [Serializable]
    public class IndexViewModel : ViewModel
    {
        public IEnumerable<PostDisplay> Posts { get; set; }
    }
}