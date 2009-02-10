using Fohjin.Core.Domain;

namespace Fohjin.Core.Web.DisplayModels
{
    public class BlogPostLinkDisplay
    {
        public BlogPostLinkDisplay(Post post)
        {
            Post = new PostDisplay(post);
        }

        public PostDisplay Post { get; private set; }
    }
}