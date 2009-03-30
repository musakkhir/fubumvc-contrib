using System;
using System.Collections.Generic;
using System.Linq;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;
using AltOxite.Core.Services;
using AltOxite.Core.Web.Controllers;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Controller.Results;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using FubuMVC.Core;

namespace AltOxite.Tests.Web.Controllers
{
    [TestFixture]
    public class BlogPostControllerTester
    {
        private IRepository _repository;
        private BlogPostController _controller;
        private IBlogPostCommentService _blogPostCommentService;
        private IUserService _userService;
        private IList<Post> _posts;
        private IUrlResolver _resolver;
        private Post _post;
        private string _testSlug;
        private ITagService _tagService;

        [SetUp]
        public void SetUp()
        {
            _posts = new List<Post>();
            _repository = MockRepository.GenerateStub<IRepository>();
            _resolver = MockRepository.GenerateStub<IUrlResolver>();
            _blogPostCommentService = MockRepository.GenerateStub<IBlogPostCommentService>();
            _userService = MockRepository.GenerateStub<IUserService>();
            _tagService = MockRepository.GenerateStub<ITagService>();
            _controller = new BlogPostController(_repository, _resolver, _blogPostCommentService, _userService, _tagService);

            _testSlug = "TESTSLUG";

            _post = new Post { Slug = _testSlug };
            _posts.Add(_post);
        }

        [Test]
        public void index_should_do_nothing_but_just_render_the_view_if_nothing_was_supplied()
        {
            var output = _controller.Index(new BlogPostViewModel());
            
            output.Post.ShouldBeNull();
            _repository.AssertWasNotCalled(r=>r.Query<Post>());
        }

        [Test]
        public void should_do_nothing_repository_returns_no_results_for_query()
        {
            _repository.Stub(r => r.Query<Post>(null)).IgnoreArguments().Return(new Post[0].AsQueryable());

            var output = _controller.Index(new BlogPostViewModel {Slug = "badslug"});

            output.Post.ShouldBeNull();
        }
    }

    [TestFixture]
    public class BlogPostController_when_an_anonymous_user_adds_a_comment
    {
        private IRepository _repository;
        private BlogPostController _controller;
        private IBlogPostCommentService _blogPostCommentService;
        private IUserService _userService;
        private string _testSlug;
        private BlogPostCommentViewModel _validInput;
        private BlogPostCommentViewModel _invalidInput;
        private IList<Post> _posts;
        private IList<User> _users;
        private IUrlResolver _resolver;
        private Post _post;
        private ITagService _tagService;

        [SetUp]
        public void SetUp()
        {
            _posts = new List<Post>();
            _users = new List<User>();
            _repository = MockRepository.GenerateStub<IRepository>();
            _resolver = MockRepository.GenerateStub<IUrlResolver>();
            _blogPostCommentService = MockRepository.GenerateStub<IBlogPostCommentService>();
            _userService = MockRepository.GenerateStub<IUserService>();
            _tagService = MockRepository.GenerateStub<ITagService>();
            _controller = new BlogPostController(_repository, _resolver, _blogPostCommentService, _userService, _tagService);
            
            _post = new Post { Slug = _testSlug };
            _posts.Add(_post);

            _repository
               .Stub(r => r.Query<Post>(null))
               .IgnoreArguments()
               .Return(_posts.AsQueryable());

            _repository
               .Stub(r => r.Query<User>(null))
               .IgnoreArguments()
               .Return(_users.AsQueryable());

            _testSlug = "TESTSLUG";

            _invalidInput = new BlogPostCommentViewModel {Slug = _testSlug};
            
            _validInput = new BlogPostCommentViewModel
            {
                UserDisplayName = "username",
                UserEmail = "email",
                Body = "body",
                UserSubscribed = true,
                Slug = _testSlug
            };
        }

        [Test]
        public void should_return_validation_error_if_name_not_specified()
        {
            var output = _controller.Comment(_invalidInput);

            output.InvalidFields.Count.ShouldBeGreaterThan(0);
        }

        [Test]
        public void should_return_the_field_name_of_the_invalid_field()
        {
            var output = _controller.Comment(_invalidInput);

            output.InvalidFields[0].ShouldEqual("UserDisplayName");
        }

        [Test]
        public void should_load_the_post_by_the_given_slug()
        {
            _controller.Comment(_validInput);

            _repository.AssertWasCalled(
                r=>r.Query<Post>(null),
                o => o.Constraints(Property.Value("Slug", _testSlug)));
        }
    }

    [TestFixture]
    public class UserService_when_creating_a_new_anonymous_user
    {
        private IRepository _repository;
        private User _curUser;
        private IList<User> _users;
        private IUserService _userService;
        private string GivenUserDisplayName;
        private string GivenUserEmail;
        private string GivenUserUrl;

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>();

            _repository = MockRepository.GenerateStub<IRepository>();
            _userService = new UserService(_repository);

            _curUser = null;

            _repository.Stub(r => r.Query<User>(null)).IgnoreArguments().Return(_users.AsQueryable());

            GivenUserDisplayName = "username";
            GivenUserEmail = "email";
            GivenUserUrl = "www";

            _curUser = _userService.AddOrUpdateUser(GivenUserEmail, GivenUserDisplayName, GivenUserUrl);
        }

        public User CreatedUser
        {
            get
            {
                if( _curUser == null )
                {
                    _curUser = _userService.AddOrUpdateUser(GivenUserEmail, GivenUserDisplayName, GivenUserUrl);
                }
                return _curUser;
            }
        }

        [Test]
        public void the_user_should_have_been_created()
        {
            CreatedUser.ShouldNotBeNull();
        }

        [Test]
        public void the_user_should_be_anonymous()
        {
            CreatedUser.IsAuthenticated.ShouldBeFalse();
        }

        [Test]
        public void should_have_a_hashed_gravatar_email()
        {
            CreatedUser.HashedEmail.ShouldNotBeNull();
            CreatedUser.HashedEmail.ShouldNotEqual(GivenUserEmail);
        }

        [Test]
        public void should_have_a_url()
        {
            CreatedUser.Url.ShouldNotBeNull();
            CreatedUser.Url.ShouldContain(GivenUserUrl);
        }
    }

    [TestFixture]
    public class BlogPostController_when_updating_an_anonymous_user_for_new_comment
    {
        private IRepository _repository;
        private IList<User> _users;
        private IUserService _userService;
        private string GivenUserDisplayName;
        private string GivenUserEmail;
        private string GivenUserUrl;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>
            { 
                new User
                {
                    DisplayName = "prev_username",
                    Url = "prev_url"
                }
            };

            _repository = MockRepository.GenerateStub<IRepository>();
            _userService = new UserService(_repository);

            _repository.Stub(r => r.Query<User>(null)).IgnoreArguments().Return(_users.AsQueryable());

            GivenUserDisplayName = "username";
            GivenUserEmail = "email";
            GivenUserUrl = "url";

            _user = _userService.AddOrUpdateUser(GivenUserEmail, GivenUserDisplayName, GivenUserUrl);
        }

        public BlogPostCommentViewModel Given { get; set; }

        [Test]
        public void the_display_name_should_have_been_updated()
        {
            _user.DisplayName.ShouldEqual(GivenUserDisplayName);
        }

        [Test]
        public void the_url_should_have_been_updated()
        {
            _user.Url.ShouldContain(GivenUserUrl);
        }
    }

    [TestFixture]
    public class BlogPostController_when_adding_a_new_post
    {
        private BlogPostController _controller;
        private IRepository _repository;
        private IUrlResolver _resolver;
        private IBlogPostCommentService _blogPostCommentService;
        private IUserService _userService;
        private ITagService _tagService;

        [SetUp]
        public void Setup()
        {
            _repository = MockRepository.GenerateStub<IRepository>();
            _resolver = MockRepository.GenerateStub<IUrlResolver>();
            _blogPostCommentService = MockRepository.GenerateStub<IBlogPostCommentService>();
            _userService = MockRepository.GenerateStub<IUserService>();
            _tagService = MockRepository.GenerateStub<ITagService>();
            _controller = new BlogPostController(_repository, _resolver, _blogPostCommentService, _userService, _tagService);
            
        }

        [Test]
        public void Should_render_the_view_when_nothing_is_supplied()
        {
            var outModel = _controller.Add(new BlogPostAddViewModel());

            outModel.ShouldBeOfType<BlogPostAddViewModel>();
            
        }
    }

    [TestFixture]
    public class BlogPostController_when_Editing_an_existing_post
    {
        private BlogPostController _controller;
        private IRepository _repository;
        private IUrlResolver _resolver;
        private IBlogPostCommentService _blogPostCommentService;
        private IUserService _userService;
        private ITagService _tagService;

        [SetUp]
        public void Setup()
        {
            _repository = MockRepository.GenerateMock<IRepository>();
            _resolver = MockRepository.GenerateStub<IUrlResolver>();
            _blogPostCommentService = MockRepository.GenerateStub<IBlogPostCommentService>();
            _userService = MockRepository.GenerateStub<IUserService>();
            _tagService = MockRepository.GenerateStub<ITagService>();
            _controller = new BlogPostController(_repository, _resolver, _blogPostCommentService, _userService, _tagService);

        }

        [Test]
        public void Should_return_a_bad_redirect_result_when_nothing_is_supplied()
        {
            var outModel = _controller.Edit(new BlogPostEditViewModel());

            outModel.ShouldBeOfType<BlogPostAddViewModel>();

            var actualResult = outModel as BlogPostAddViewModel;

            actualResult.ShouldNotBeNull();
            actualResult.ResultOverride.ShouldBeOfType<RedirectResult>();

        }

        [Test]
        [Ignore("Can't get this working right now - RK")]
        public void Should_return_a_bad_redirect_result_if_nothing_is_returned_from_Repository()
        {
            var testSlug = "My-Test-Slug";
            var postList = new List<Post> {new Post {ID = Guid.NewGuid(), Title = "My Title"}}.AsQueryable();
            
            // Problem is with this somewhere
            //_repository.Stub(call => call.Query(new PostBySlug(testSlug))).IgnoreArguments().Return(postList);
            _repository.Expect(call => call.Query(new PostBySlug(testSlug))).IgnoreArguments().Return(postList);
            _repository.Expect(call => call.Query(new PostBySlug(testSlug)).SingleOrDefault()).IgnoreArguments().Return(null);
            
            var outModel = _controller.Edit(new BlogPostEditViewModel { Slug = testSlug});

            outModel.ShouldBeOfType<BlogPostAddViewModel>();

            var actualResult = outModel as BlogPostAddViewModel;

            actualResult.ShouldNotBeNull();
            actualResult.ResultOverride.ShouldBeOfType<RedirectResult>();
        }

        [Test]
        public void Should_render_the_view_and_have_valid_data_when_Valid_Slug_is_passed_in()
        {
            var testSlug = "My-Test-Slug";
            var id = Guid.NewGuid();
            var title = "My BlogPost Title";
            var bodyShort = "Body Short Text";
            var body = "This is the main body of a blogpost";
            var published = DateTime.Today;
            var modelTags = "help, test, sample, ";
            var objectTags = new List<Tag> {new Tag {Name = "help"}, new Tag {Name = "test"}, new Tag {Name = "sample"}};

            var post = new Post
                           {
                               ID = id,
                               Title = title,
                               BodyShort = bodyShort,
                               Body = body,
                               Published = published
                           };
            objectTags.Each(post.AddTag);

            var postList = new List<Post> { post }.AsQueryable();

            _repository.Expect(call => call.Query(new PostBySlug(testSlug))).IgnoreArguments().Return(postList);

            var outModel = _controller.Edit(new BlogPostEditViewModel { Slug = testSlug });

            outModel.ShouldBeOfType<BlogPostAddViewModel>();

            var actualResult = outModel as BlogPostAddViewModel;

            actualResult.ShouldNotBeNull();

            actualResult.Id.ShouldEqual(id.ToString());
            actualResult.Title.ShouldEqual(title);
            actualResult.BodyShort.ShouldEqual(bodyShort);
            actualResult.Body.ShouldEqual(body);
            actualResult.Published.ShouldEqual(published);
            actualResult.Tags.ShouldEqual(modelTags);
            
        }
    }
}