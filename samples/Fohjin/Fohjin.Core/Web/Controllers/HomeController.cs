using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Persistence;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core;

namespace Fohjin.Core.Web.Controllers
{
    public class HomeController
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IndexViewModel Index(IndexSetupViewModel inModel)
        {
            const int pageSizeToShow = 5;
            const int pageSizeToGet = 8;

            if (inModel.Page == 0)
                inModel.Page = 1;

            var posts = (inModel.Page == 1) 
                ? _repository.Query<Post>().OrderByDescending(p => p.Published).Take(pageSizeToGet).ToList() 
                : _repository.Query<Post>().OrderByDescending(p => p.Published).Skip(pageSizeToShow * (inModel.Page - 1)).Take(pageSizeToGet).ToList();

            bool showPreviousPageLink = !(inModel.Page == 1);
            bool showNextPageLink = posts.Count() == pageSizeToGet;
            var pageSize = showNextPageLink ? pageSizeToShow : pageSizeToGet;

            return new IndexViewModel
            {
                Posts = posts.ToList().Take(pageSize).Select(p => new PostDisplay(p)),
                SiteName = "{0} - Opinionated, let me tell you about it".ToFormat(inModel.SiteName),
                ShowNextPageLink = showNextPageLink,
                ShowPreviousPageLink = showPreviousPageLink,
                NextPage = inModel.Page + 1,
                PreviousPage = inModel.Page - 1,
                ShowBothLinks = showNextPageLink && showPreviousPageLink,
            };
        }
    }

    public class IndexSetupViewModel : ViewModel
    {
        public int Page { get; set; }
    }

    [Serializable]
    public class IndexViewModel : ViewModel
    {
        public IEnumerable<PostDisplay> Posts { get; set; }
        public bool ShowPreviousPageLink { get; set; }
        public bool ShowNextPageLink { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public bool ShowBothLinks { get; set; }
    }
}