using Fohjin.Core.Domain;
using Fohjin.Core.Services;
using FubuMVC.Core.Controller.Config;
using NUnit.Framework;
using Rhino.Mocks;

namespace Fohjin.Tests.Services
{
    [TestFixture]
    public class TwitterServiceTester
    {
        private SiteConfiguration _siteConfiguration;
        private TwitterService _twitterService;
        private User _user;
        private ITwitterClient _twitterClient;
        private Post _post;
        private ITinyUrlService _tinyUrlService;
        private IUrlResolver _urlResolver;

        [SetUp]
        public void SetUp()
        {
            _siteConfiguration = new SiteConfiguration
            {
                TwitterUserName = "TestUser",
                TwitterPassword = "TestPassword",
            };

            _twitterClient = MockRepository.GenerateStub<ITwitterClient>();
            _tinyUrlService = MockRepository.GenerateStub<ITinyUrlService>();
            _urlResolver = MockRepository.GenerateStub<IUrlResolver>();

            _twitterService = new TwitterService(_siteConfiguration, _twitterClient, _tinyUrlService, _urlResolver);

            _user = new User
            {
                TwitterUserName = "MarkNijhof",
            };

            _post = new Post
            {
                User = _user,
                Title = "Test title",
            };
        }

        [Test]
        public void Should_be_able_to_send_a_message_to_a_twitter_user_from_the_website_account()
        {
            const string message = "Test Message";

            _twitterClient.Stub<ITwitterClient>(x => x.SendReply("", "", "", "")).IgnoreArguments().Return(true);

            _twitterService.SendCommentPostedReply(_post, message, "mark nijhof").ShouldEqual(true);

            _twitterClient.AssertWasCalled(x => x.SendReply("", "", "", ""), x => x.IgnoreArguments());
        }
    }
}