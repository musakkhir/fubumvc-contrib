using Fohjin.Core.Domain;

namespace Fohjin.Core.Services
{
    public interface IBlogPostCommentService 
    {
        void AddCommentToBlogPost(string body, bool userSubscribed, User user, Post post, string twitterUserName);
    }
}