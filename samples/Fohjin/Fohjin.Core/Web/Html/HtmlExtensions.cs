using System.Collections.Generic;
using Fohjin.Core.Domain;
using FubuMVC.Core.Controller.Config;
using Microsoft.Practices.ServiceLocation;
using FubuMVC.Core.Html;
using FubuMVC.Core.Html.Expressions;
using FubuMVC.Core.View.WebForms;

namespace Fohjin.Core.Web.Html
{
    public static class HtmlExtensions
    {
        public static LinkExpression SkinCSS(this IFohjinPage viewPage, string url)
        {
            var siteConfig = ServiceLocator.Current.GetInstance<SiteConfiguration>();
            var baseUrl = siteConfig.CssPath;
            return viewPage.CSS(url).BasedAt(baseUrl);
        }

        public static ScriptReferenceExpression SkinScript(this IFohjinPage viewPage, string url)
        {
            var siteConfig = ServiceLocator.Current.GetInstance<SiteConfiguration>();
            var baseUrl = siteConfig.ScriptsPath;
            return viewPage.Script(url).BasedAt(baseUrl);
        }

        public static ScriptReferenceExpression SkinScript(this IFohjinPage viewPage, IEnumerable<string> urls)
        {
            var siteConfig = ServiceLocator.Current.GetInstance<SiteConfiguration>();
            var baseUrl = siteConfig.ScriptsPath;
            return viewPage.Script(urls).BasedAt(baseUrl);
        }

        public static LoginStatusExpression DisplayDependingOnLoginStatus(this IFohjinPage viewPage)
        {
            var renderer = ServiceLocator.Current.GetInstance<IWebFormsViewRenderer>();
            var conventions = ServiceLocator.Current.GetInstance<FubuConventions>();
            return new LoginStatusExpression(viewPage, renderer, conventions);
        }
    }
}