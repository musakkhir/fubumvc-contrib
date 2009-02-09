using System.Linq;
using Fohjin.Core.Domain;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Core;
using Microsoft.Practices.ServiceLocation;

namespace Fohjin.Core.Web.Html
{
    public static class BlogPostHtmlExtension
    {
        public static string GetCommentsText(this IFohjinPage viewPage, PostDisplay post)
        {
           var commentCount = (post.Comments == null) ? 0 : post.Comments.Count();
            return "<a href=\"{0}#comments\">{1}</a>"
                .ToFormat(
                    viewPage.UrlTo().PublishedPost(post),
                   (commentCount == 1)
                       ? "{0} comment".ToFormat(commentCount)
                       : "{0} comments".ToFormat(commentCount));
        }

        public static string GetGravatarImage(this IFohjinPage viewPage, User user)
        {
            var siteConfig = ServiceLocator.Current.GetInstance<SiteConfiguration>();

            return "<img alt=\"{0} (gravatar)\" src=\"{1}\" />"
                .ToFormat(
                    user.DisplayName, 
                    "http://www.gravatar.com/avatar/{0}?s=80&amp;default={1}".ToFormat(
                        user.HashedEmail, 
                        siteConfig.GravatarDefault));
        }

        public static string GetCommentPremalink(this IFohjinPage viewPage, CommentDisplay comment)
        {
            return (!string.IsNullOrEmpty(comment.PermalinkHash)) 
                ? "<a href=\"#{0}\">{1}</a>".ToFormat(comment.PermalinkHash, comment.LocalPublishedDate)
                : "";
        }

        public static string GetCommentPremalinkBookmark(this IFohjinPage viewPage, CommentDisplay comment)
        {
            return (!string.IsNullOrEmpty(comment.PermalinkHash)) 
                ? "<div><a name=\"{0}\"></a></div>".ToFormat(comment.PermalinkHash)
                : "";
        }

        public static string GetCommenterGravatarAndLink(this IFohjinPage viewPage, CommentDisplay comment)
        {
            return (!string.IsNullOrEmpty(comment.User.Url))
                ? "<a class=\"avatar\" href=\"{0}\" rel=\"nofollow\">{1}</a>".ToFormat(comment.User.Url, viewPage.GetGravatarImage(comment.User))
                : viewPage.GetGravatarImage(comment.User);
        }

        public static string GetCommenterNameAndLink(this IFohjinPage viewPage, CommentDisplay comment)
        {
            return (!string.IsNullOrEmpty(comment.User.Url))
                ? "<a href=\"{0}\">{1}</a>".ToFormat(comment.User.Url, comment.User.DisplayName)
                : comment.User.DisplayName;
        }

        public static string GetPuzzlePieceImage(this IFohjinPage viewPage, int dateValue)
        {
            string color = "all";
            if (dateValue % 4 == 0) color = "green";
            if (dateValue % 4 == 1) color = "blue";
            if (dateValue % 4 == 2) color = "red";
            if (dateValue % 4 == 3) color = "dark_blue";
            return "<div class=\"left\" id=\"headerimage\" style=\"background-image:url(/content/images/nda/puzzle_45_60_{0}.jpg);background-position: top left;background-repeat: no-repeat;\"></div>".ToFormat(color);
        }
    }
}
