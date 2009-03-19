using System;
using System.Linq;
using System.Web;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Services;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Controller;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Controller.Results;
using FubuMVC.Validation.Results;

namespace Fohjin.Core.Web.Controllers
{
    public class BlogPostController
    {
        private readonly IRepository _repository;
        private readonly IUrlResolver _resolver;
        private readonly IBlogPostCommentService _blogPostCommentService;
        private readonly IUserService _userService;

        public BlogPostController(IRepository repository, IUrlResolver resolver, IBlogPostCommentService blogPostCommentService, IUserService userService)
        {
            _repository = repository;
            _resolver = resolver;
            _blogPostCommentService = blogPostCommentService;
            _userService = userService;
        }

        public BlogPostViewModel Index(BlogPostViewModel inModel)
        {
            var badRedirectResult = new BlogPostViewModel { ResultOverride = new RedirectResult(_resolver.PageNotFound()) };

            if (inModel.Slug.IsEmpty()) return badRedirectResult;

            var post = _repository.Query(new PostBySlug(inModel.Slug)).SingleOrDefault();

            if (post == null) return badRedirectResult;

            User user = inModel.CurrentUser;
            
            var postDisplay = new PostDisplay(post);
            return new BlogPostViewModel
            {
                Post = postDisplay,
                Comment = new CommentFormDisplay(new Comment { User = user }, postDisplay)
            };
        }

        public BlogPostViewModel Comment(BlogPostCommentViewModel inModel)
        {
            var badRedirectResult = new BlogPostViewModel{ResultOverride = new RedirectResult(_resolver.PageNotFound())};

            if (inModel.Slug.IsEmpty()) return badRedirectResult;

            var post = _repository.Query(new PostBySlug(inModel.Slug)).SingleOrDefault();

            if (post == null) return badRedirectResult;

            var postDisplay = new PostDisplay(post);

            if (inModel.ValidationResults.GetInvalidFields().Count() > 0)
            {
                var bblo = new BlogPostViewModel
                {
                    Post = postDisplay,
                    Comment = new CommentFormDisplay(new Comment
                    {
                        Body = inModel.Body,
                        UserSubscribed = inModel.Subscribed,
                        User = new User
                        {
                            DisplayName = inModel.DisplayName,
                            Email = inModel.Email,
                            Url = inModel.OptionalUrl,
                        }, 
                    }, postDisplay),
                    //ResultOverride = new RedirectResult(_resolver.PublishedPost(postDisplay) + "#leave_comment"),
                };
                bblo.Comment.ValidationResults.CloneFrom(inModel.ValidationResults);
                return bblo;
            }

            var user = _userService.AddOrUpdateUser(inModel.Email, HttpUtility.HtmlEncode(inModel.DisplayName), HttpUtility.HtmlEncode(inModel.OptionalUrl));
            _blogPostCommentService.AddCommentToBlogPost(HttpUtility.HtmlEncode(inModel.Body), inModel.Subscribed, user, post);

            return new BlogPostViewModel {ResultOverride = new RedirectResult(_resolver.PublishedPost(postDisplay))};
        }
    }

    [Serializable]
    public class BlogPostViewModel : ViewModel, ISupportResultOverride
    {
        public IInvocationResult ResultOverride { get; set; }

        [Required]public int PostYear { get; set; }
        [Required]public int PostMonth { get; set; }
        [Required]public int PostDay { get; set; }
        [Required]public string Slug { get; set; }
        public PostDisplay Post { get; set; }
        public CommentFormDisplay Comment { get; set; }
    }

    [Serializable]
    public class BlogPostCommentViewModel : ICanBeValidated
    {
        [Required]public int PostYear { get; set; }
        [Required]public int PostMonth { get; set; }
        [Required]public int PostDay { get; set; }
        [Required]public string Slug { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public string OptionalUrl { get; set; }
        public bool Remember { get; set; }
        public bool Subscribed { get; set; }

        private readonly IValidationResults _validationResults = new ValidationResults();
        public IValidationResults ValidationResults
        {
            get { return _validationResults; }
        }
    }
}