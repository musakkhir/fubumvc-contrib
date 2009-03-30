using System;
using System.Collections.Generic;
using System.Linq;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;
using AltOxite.Core.Web.DisplayModels;

namespace AltOxite.Core.Web.Controllers
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
            // Todo: Is this what we want here to only show published posts?
            var posts = _repository.Query<Post>().Where(post=> post.Published >= DateTime.Today).OrderBy(post => post.Published);
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