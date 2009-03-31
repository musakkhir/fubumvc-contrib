using System.Collections.Generic;
using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Web;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core;
using FubuMVC.Core.Controller.Config;

namespace Fohjin.Core.Services
{
    public interface ITwitterService 
    {
        bool SendCommentPostedReply(Post post, string twitterUserName, string displayName);
    }

    public class TwitterService : ITwitterService
    {
        private readonly SiteConfiguration _siteConfiguration;
        private readonly ITwitterClient _twitterClient;
        private readonly ITinyUrlService _tinyUrlService;
        private readonly IUrlResolver _urlResolver;

        public TwitterService(SiteConfiguration siteConfiguration, ITwitterClient twitterClient, ITinyUrlService tinyUrlService, IUrlResolver urlResolver)
        {
            _urlResolver = urlResolver;
            _tinyUrlService = tinyUrlService;
            _siteConfiguration = siteConfiguration;
            _twitterClient = twitterClient;
        }

        public bool SendCommentPostedReply(Post post, string twitterUserName, string displayName)
        {
            var pageUrl = _tinyUrlService.Tinyfy(_urlResolver.PublishedPost(new PostDisplay(post)));
            var pageTitle = post.Title;
            if (pageTitle.Length > 50)
                pageTitle = pageTitle.Substring(0, 47) + "...";

            var message = "New comment from {0} in {1} - {2}".ToFormat(displayName, pageTitle, pageUrl);

            List<string> twitterUserNames = post.GetComments()
                .Where(comment => (!string.IsNullOrEmpty(comment.TwitterUserName) && comment.TwitterUserName != twitterUserName))
                .Select(x => x.TwitterUserName)
                .ToList();

            twitterUserNames.Add(post.User.TwitterUserName);

            twitterUserNames.Distinct().Each(userName =>
                _twitterClient.SendReply(_siteConfiguration.TwitterUserName, _siteConfiguration.TwitterPassword, userName, message));

            return true;
        }
    }
}