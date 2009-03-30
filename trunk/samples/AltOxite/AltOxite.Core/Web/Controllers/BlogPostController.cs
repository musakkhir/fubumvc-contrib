using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;
using AltOxite.Core.Services;
using AltOxite.Core.Web.DisplayModels;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Controller;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Controller.Results;
using FubuMVC.Core.Util;

namespace AltOxite.Core.Web.Controllers
{
    public class BlogPostController
    {
        private readonly IRepository _repository;
        private readonly IUrlResolver _resolver;
        private readonly IBlogPostCommentService _blogPostCommentService;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;

        public BlogPostController(IRepository repository, IUrlResolver resolver, IBlogPostCommentService blogPostCommentService, IUserService userService, ITagService tagService)
        {
            _repository = repository;
            _resolver = resolver;
            _blogPostCommentService = blogPostCommentService;
            _userService = userService;
            _tagService = tagService;
        }

        public BlogPostViewModel Index(BlogPostViewModel inModel)
        {
            var badRedirectResult = new BlogPostViewModel { ResultOverride = new RedirectResult(_resolver.PageNotFoundUrl()) };

            if (inModel.Slug.IsEmpty()) return badRedirectResult;

            var post = _repository.Query(new PostBySlug(inModel.Slug)).SingleOrDefault();

            if (post == null) return badRedirectResult;

            User user = inModel.CurrentUser;
            
            var postDisplay = new PostDisplay(post);
            return new BlogPostViewModel
            {
                Post = postDisplay,
                Comment = new CommentFormDisplay(user, new Comment(), postDisplay)
            };
        }

        public BlogPostAddViewModel Add(BlogPostAddViewModel inModel)
        {
            return new BlogPostAddViewModel();
        }

        public BlogPostAddViewModel Edit(BlogPostEditViewModel inModel)
        {
            var badRedirectResult = new BlogPostAddViewModel { ResultOverride = new RedirectResult(_resolver.PageNotFoundUrl()) };

            var outModel = new BlogPostAddViewModel();

            if (inModel.Slug.IsEmpty()) return badRedirectResult;

            var post = _repository.Query(new PostBySlug(inModel.Slug)).SingleOrDefault();

            if (post == null) return badRedirectResult;

            outModel.Id = post.ID.ToString();
            outModel.Title = post.Title;
            outModel.BodyShort = post.BodyShort;
            outModel.Body = post.Body;
            outModel.Slug = post.Slug;
            outModel.Published = post.Published;
            post.GetTags().Each(tag => outModel.Tags += tag.Name + ", ");

            return outModel;
        }

        public BlogPostViewModel Save(BlogPostAddViewModel inModel)
        {
            var post = new Post();

            if(inModel.Id.IsNotEmpty())
            {
                post = _repository.Load<Post>(new Guid(inModel.Id));
            }

            post.Title = inModel.Title;
            post.BodyShort = inModel.BodyShort;
            post.Body = inModel.Body;
            post.Slug = inModel.Slug; // Looks like oxite is doing this in jQuery so don't need: post.Slug.IsEmpty() ? _slugService.CreateSlugFromText(inModel.Slug.IsEmpty() ? inModel.Title : inModel.Slug) : post.Slug;
            post.User = inModel.CurrentUser;
            post.Published = inModel.Published;

            if(inModel.Tags.IsNotEmpty())
            {
                var tags = inModel.Tags.Split(',');
                tags.Each(tag =>
                              {
                                  if (tag.Trim().IsNotEmpty())
                                      post.AddTag(_tagService.CreateOrGetTagForItem(tag.ToLowerInvariant().Trim()));
                              });
            }


            //Todo: Validation on input

            _repository.Save(post);

            return new BlogPostViewModel();
        }

        public BlogPostViewModel Comment(BlogPostCommentViewModel inModel)
        {
            var badRedirectResult = new BlogPostViewModel{ResultOverride = new RedirectResult(_resolver.PageNotFoundUrl())};

            if (inModel.Slug.IsEmpty()) return badRedirectResult;

            var post = _repository.Query(new PostBySlug(inModel.Slug)).SingleOrDefault();

            if (post == null) return badRedirectResult;

            var result = new BlogPostViewModel();

            valid_comment_submission(inModel, result);

            if( result.InvalidFields.Count > 0 )
            {
                result.ResultOverride = new RedirectResult(_resolver.PublishedPost(new PostDisplay(post)));
                return result;
            }

            var user = _userService.AddOrUpdateUser(inModel.UserEmail, inModel.UserDisplayName, inModel.UserUrl);
             
            _blogPostCommentService.AddCommentToBlogPost(inModel.Body, inModel.UserSubscribed, user, post);

            var postDisplay = new PostDisplay(post);

            result.ResultOverride = new RedirectResult(_resolver.PublishedPost(postDisplay));

            return result;
        }

        private static void valid_comment_submission(BlogPostCommentViewModel inModel, BlogPostViewModel result)
        {
            // TODO: have attributes on the viewmodel and do validation elsewhere 
            if (inModel.UserDisplayName.IsEmpty()) result.AddInvalidField<BlogPostCommentViewModel>(x => x.UserDisplayName);
            if (inModel.UserEmail.IsEmpty()) result.AddInvalidField<BlogPostCommentViewModel>(x => x.UserEmail);
            if (inModel.Body.IsEmpty()) result.AddInvalidField<BlogPostCommentViewModel>(x => x.Body);
        }
    }

    [Serializable]
    public class BlogPostAddViewModel : ViewModel, ISupportResultOverride
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string BodyShort { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime? Published { get; set; }
        public string Tags { get; set; }

        public IInvocationResult ResultOverride { get; set; }

    }

    [Serializable]
    public class BlogPostEditViewModel : ViewModel
    {
        [Required] public string Slug { get; set; }
    }

    [Serializable]
    public class BlogPostViewModel : ViewModel, ISupportResultOverride
    {
        public BlogPostViewModel()
        {
            InvalidFields = new List<string>();
        }

        public void AddInvalidField<MODEL>(Expression<Func<MODEL, object>> fieldExpression)
        {
            var name = ReflectionHelper.GetAccessor(fieldExpression).Name;
            InvalidFields.Add(name);
        }

        public IList<string> InvalidFields { get; set; }
        public IInvocationResult ResultOverride { get; set; }

        [Required] public int PostYear { get; set; }
        [Required] public int PostMonth { get; set; }
        [Required] public int PostDay { get; set; }
        [Required] public string Slug { get; set; }
        public PostDisplay Post { get; set; }
        public CommentFormDisplay Comment { get; set; }
    }

    [Serializable]
    public class BlogPostCommentViewModel : ViewModel
    {
        [Required] public int PostYear { get; set; }
        [Required] public int PostMonth { get; set; }
        [Required] public int PostDay { get; set; }
        [Required] public string Slug { get; set; }
        public string UserDisplayName { get; set; }
        public string UserEmail { get; set; }
        public string Body { get; set; }
        public string UserUrl { get; set; }
        public bool Remember { get; set; }
        public bool UserSubscribed { get; set; }
    }
}