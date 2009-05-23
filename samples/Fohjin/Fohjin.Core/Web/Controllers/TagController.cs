using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core;

namespace Fohjin.Core.Web.Controllers
{
    public class TagController
    {
        private readonly IRepository _repository;

        public TagController(IRepository repository)
        {
            _repository = repository;
        }

        public TagViewModel Index(TagSetupViewModel inModel)
        {
            if (inModel.Tag.IsEmpty()) return new TagViewModel();

            var tag = _repository.Query<Tag>().Where(p => p.Name == inModel.Tag).FirstOrDefault(); // TODO: Currently tags are not unique 

            if (tag == null) return new TagViewModel();

            const int pageSizeToShow = 15;
            const int pageSizeToGet = 18;

            if (inModel.Page == 0)
                inModel.Page = 1;

            var posts = (inModel.Page == 1)
                ? _repository.Query<Post>().OrderByDescending(p => p.Published).ToList().Where(p => p.GetTags().Contains(tag)).Take(pageSizeToGet).ToList()
                : _repository.Query<Post>().OrderByDescending(p => p.Published).ToList().Where(p => p.GetTags().Contains(tag)).Skip(pageSizeToShow * (inModel.Page - 1)).Take(pageSizeToGet).ToList();

            bool showPreviousPageLink = !(inModel.Page == 1);
            bool showNextPageLink = posts.Count() == pageSizeToGet;
            var pageSize = showNextPageLink ? pageSizeToShow : pageSizeToGet;

            return new TagViewModel
            {
                Tag = tag,
                Posts = posts.ToList().Take(pageSize).Select(p => new TagPostDisplay(p)),
                SiteName = "{0} - Opinionated, let me tell you about it".ToFormat(inModel.SiteName),
                ShowNextPageLink = showNextPageLink,
                ShowPreviousPageLink = showPreviousPageLink,
                NextPage = inModel.Page + 1,
                PreviousPage = inModel.Page - 1,
                ShowBothLinks = showNextPageLink && showPreviousPageLink,
            };
        }
    }

    public class TagSetupViewModel : ViewModel
    {
        public string Tag { get; set; }
        public int Page { get; set; }
    }

    [Serializable]
    public class TagViewModel : ViewModel
    {
        public Tag Tag { get; set; }
        public IEnumerable<TagPostDisplay> Posts { get; set; }
        public bool ShowPreviousPageLink { get; set; }
        public bool ShowNextPageLink { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public bool ShowBothLinks { get; set; }
    }
}