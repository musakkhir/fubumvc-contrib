using Microsoft.Practices.ServiceLocation;
using FubuMVC.Core.Controller.Config;

namespace Fohjin.Core.Web
{
    public static class UrlToExtensions
    {
        public static IUrlResolver UrlTo(this IFohjinPage viewPage)
        {
            return ServiceLocator.Current.GetInstance<IUrlResolver>();
        }
    }
}
