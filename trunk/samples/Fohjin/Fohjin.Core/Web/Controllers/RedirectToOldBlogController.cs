using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Controller;
using FubuMVC.Core.Controller.Results;
using FubuMVC.Core.Routing;

namespace Fohjin.Core.Web.Controllers
{
    public class RedirectToOldBlogController
    {
        private readonly ICurrentRequest _currentRequest;

        public RedirectToOldBlogController(ICurrentRequest currentRequest)
        {
            _currentRequest = currentRequest;
        }

        public RedirectToOldBlogViewModel Index(RedirectToOldBlogSetupViewModel inModel)
        {
            var currentUrl = _currentRequest.GetUrl().AbsoluteUri;
            var newUrl = currentUrl.Replace("blog.fohjin.com", "fohjin.blogspot.com");

            return new RedirectToOldBlogViewModel { ResultOverride = new RedirectResult(newUrl) };
        }
    }

    public class RedirectToOldBlogSetupViewModel
    {        
    }

    public class RedirectToOldBlogViewModel : ViewModel, ISupportResultOverride
    {
        public IInvocationResult ResultOverride { get; set; }
    }
}