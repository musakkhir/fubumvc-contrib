using System;
using System.Collections.Generic;
using System.Linq;
using AltOxite.Core.Services;
using AltOxite.Core.Web.DisplayModels;

namespace AltOxite.Core.Web.Controllers
{
    public class SiteController
    {
        private readonly IPostService _postService;

        public SiteController(IPostService postService)
        {
            _postService = postService;
        }

        public AdminDataViewModel Dashboard(ViewModel inModel)
        {
            return new AdminDataViewModel {Posts = _postService.GetPostsWithDrafts().Select(x => new PostDisplay(x))};
        }
    }

    [Serializable]
    public class AdminDataViewModel : ViewModel
    {
        public IEnumerable<PostDisplay> Posts { get; set;} 
        
    }
}