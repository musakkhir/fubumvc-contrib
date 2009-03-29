using System;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;

namespace Fohjin.Core.Services
{
    public class BlogPostCommentService : IBlogPostCommentService
    {
        private readonly IRepository _repository;
        private readonly ITwitterService _twitterService;

        public BlogPostCommentService(IRepository repository, ITwitterService twitterService)
        {
            _twitterService = twitterService;
            _repository = repository;
        }

        public void AddCommentToBlogPost(string body, bool userSubscribed, User user, Post post, string twitterUserName)
        {
            var comment = new Comment
            {
                Body = body,
                User = user,
                Post = post,
                UserSubscribed = userSubscribed,
                TwitterUserName = twitterUserName,
            };

            //TODO: Need to implement publishing/pending stuff
            comment.Published = DateTime.UtcNow;

            post.AddComment(comment);
            _repository.Save(post);

            _twitterService.SendCommentPostedReply(post, twitterUserName, user.DisplayName);
        }
    }
}