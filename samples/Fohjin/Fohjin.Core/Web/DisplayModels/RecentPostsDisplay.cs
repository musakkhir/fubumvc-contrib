using System.Collections.Generic;
using System.Linq;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Web.DisplayModels
{
    public class RecentPostsDisplay
    {
        private readonly List<BlogPostLinkDisplay> _posts = new List<BlogPostLinkDisplay>();

        public RecentPostsDisplay(List<Post> posts)
        {
            posts.ForEach(post => _posts.Add(new BlogPostLinkDisplay(post)));
        }

        public IEnumerable<BlogPostLinkDisplay> GetPosts()
        {
            return _posts.AsEnumerable();
        }
    }
}