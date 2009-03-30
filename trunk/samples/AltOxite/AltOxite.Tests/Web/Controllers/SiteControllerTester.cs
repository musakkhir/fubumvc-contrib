using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;
using AltOxite.Core.Services;
using AltOxite.Core.Web;
using AltOxite.Core.Web.Controllers;
using FubuMVC.Core.Controller.Config;
using NUnit.Framework;
using Rhino.Mocks;

namespace AltOxite.Tests.Web.Controllers
{
    [TestFixture]
    public class SiteControllerTester
    {
        private SiteController _controller;
        private IPostService _postService;
        private IList<Post> _posts;
        private Post _post;

        [SetUp]
        public void SetUp()
        {
            _posts = new List<Post>();
            _post = new Post { Title = "Test Title", Body= "This is a test post" };
            _posts.Add(_post);

            _postService = MockRepository.GenerateMock<IPostService>();
            _controller = new SiteController(_postService);

        }
        
        [Test]
        public void Dashboard_Should_Do_Nothing_but_render_the_view_when_nothing_is_supplied()
        {
            _postService.Expect(call => call.GetPostsWithDrafts()).Return(_posts.AsQueryable());
            var outModel = _controller.Dashboard(new ViewModel());

            outModel.ShouldBeOfType<AdminDataViewModel>();
            outModel.Posts.ShouldNotBeEmpty();
        }

    }
}