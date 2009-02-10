using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core.Behaviors;

namespace Fohjin.Core.Web.Behaviors
{
    public class get_recent_blog_posts : behavior_base_for_convenience
    {
        private readonly IRepository _repository;

        public get_recent_blog_posts(IRepository repository)
        {
            _repository = repository;
        }

        public void UpdateModel(ViewModel model)
        {
            if (model == null) return;

            var recentPosts = _repository.Query<Post>().OrderByDescending(p => p.Published).Take(10).ToList();
            model.RecentPosts = new RecentPostsDisplay(recentPosts);
        }

        public override void PrepareInput<INPUT>(INPUT input)
        {
            UpdateModel(input as ViewModel);
        }
    }
}