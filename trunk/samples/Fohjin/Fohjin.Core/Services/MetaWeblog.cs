using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Web;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core;
using FubuMVC.Core.Controller.Config;

namespace Fohjin.Core.Services
{
    public class MetaWeblog : IMetaWeblog
    {
        private readonly IRepository _repository;
        private readonly IUrlResolver _urlResolver;
        private readonly ISecurityDataService _securityDataService;

        public MetaWeblog(IRepository repository, IUrlResolver urlResolver, ISecurityDataService securityDataService)
        {
            _repository = repository;
            _urlResolver = urlResolver;
            _securityDataService = securityDataService;
        }

        public bool editPost(string postid, string username, string password, mwPost mwPost, bool publish)
        {
            ValidateUserCredentials(username, password);

            throw new NotImplementedException();
        }

        public mwCategoryInfo[] getCategories(object blogid, string username, string password)
        {
            ValidateUserCredentials(username, password);

            var categoryInfos = new List<mwCategoryInfo>();
            var tags = _repository.Query<Tag>();

            tags.Each(tag =>
            {
                mwCategoryInfo mwCategoryInfo = new mwCategoryInfo
                {
                    categoryid = tag.ID.ToString(),
                    description = tag.Name,
                    title = tag.Name,
                    htmlUrl = _urlResolver.Tag(tag.Name),
                    rssUrl = ""
                };
                categoryInfos.Add(mwCategoryInfo);
            });

            return categoryInfos.ToArray();
        }

        public mwPost getPost(string postid, string username, string password)
        {
            ValidateUserCredentials(username, password);

            var post = _repository.Load<Post>(new Guid(postid));
            if (post == null)
                throw new Exception("Invalid post id passed");

            return Convert(post);
        }

        public mwPost[] getRecentPosts(object blogid, string username, string password, int numberOfPosts)
        {
            ValidateUserCredentials(username, password);

            var posts = _repository.Query<Post>().OrderByDescending(p => p.Published).Take(10).ToList();
            List<mwPost> mwPosts = new List<mwPost>();
            posts.Each(post => mwPosts.Add(Convert(post)));
            return mwPosts.ToArray();
        }

        public string newPost(object blogid, string username, string password, mwPost mwPost, bool publish)
        {
            throw new NotImplementedException();
        }

        public mediaObjectInfo newMediaObject(object blogid, string username, string password, mediaObject mediaobject)
        {
            throw new NotImplementedException();
        }

        public bool deletePost(string appKey, string postid, string username, string password, bool publish)
        {
            throw new NotImplementedException();
        }

        public mwBlogInfo[] getUsersBlogs(string appKey, string username, string password)
        {
            throw new NotImplementedException();
        }

        private Guid ValidateUserCredentials(string username, string password)
        {
            var userId = _securityDataService.AuthenticateForUserId(username, password);
            if (!userId.HasValue)
                throw new Exception("Invalid post id passed");

            return userId.Value;
        }

        private mwPost Convert(Post post)
        {
            var mwpost = new mwPost
            {
                dateCreated = post.Published.Value,
                description = post.Body,
                title = post.Title,
                postid = post.ID.ToString(),
                link = _urlResolver.PublishedPost(new PostDisplay(post)),
                permalink = _urlResolver.PublishedPost(new PostDisplay(post)),
                userid = post.User.ID.ToString()
            };
            List<string> categories = new List<string>();
            post.GetTags().Each(tag => categories.Add(tag.Name));
            mwpost.categories = categories.ToArray();
            return mwpost;
        }
    }
}