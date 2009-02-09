using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Fohjin.Core.Web;
using Fohjin.Core.Web.Behaviors;
using Fohjin.Core.Web.Controllers;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Container.StructureMap.Config;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Html.Expressions;

namespace Fohjin.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ControllerConfig.Configure = x =>
            {
                x.UsingConventions( conventions =>
                {
                    conventions.PartialForEachOfHeader = FohjinDefaultPartialHeader;
                    conventions.PartialForEachOfBeforeEachItem = FohjinDefaultPartialBeforeEachItem;
                    conventions.PartialForEachOfAfterEachItem = FohjinDefaultPartialAfterEachItem;
                    conventions.PartialForEachOfFooter = FohjinDefaultPartialFooter;
                });

                // Default Behaviors for all actions -- ordered as they're executed
                /////////////////////////////////////////////////
                x.ByDefault.EveryControllerAction(d => d
                    .Will<access_the_database_through_a_unit_of_work>()
                    .Will<set_up_default_data_the_first_time_this_app_is_run>()

                    .Will<set_empty_default_user_on_the_output_viewmodel_to_make_sure_one_exists>()
                    .Will<load_the_current_principal>()
                    .Will<set_the_current_logged_in_user_on_the_output_viewmodel>()
                    .Will<set_user_from_http_cookie_if_current_user_is_not_authenticated>()

                    .Will<execute_the_result>()
                    .Will<OutputAsRssOrAtomFeed>()
                    .Will<set_the_current_site_details_on_the_output_viewmodel>()
                    .Will<copy_viewmodel_from_input_to_output<ViewModel>>()
                    );
                
                // Automatic controller registration
                /////////////////////////////////////////////////
                x.AddControllersFromAssembly.ContainingType<ViewModel>(c =>
                {
                    // All objects in Web.Controllers whose name ends with "*Controller"
                    // All public OMIOMO methods are actions, so no need to filter the methods
                    c.Where(t => 
                        t.Namespace.EndsWith("Web.Controllers") 
                        && t.Name.EndsWith("Controller"));

                    c.MapActionsWhere((m,i,o) => true);
                });

                // Manual overrides
                /////////////////////////////////////////////////
                x.OverrideConfigFor(HomeIndexAction, config => config.AddRssFeedUrl());

                //-- Make the primary URL for logout be "/logout" instead of "login/logout"
                x.OverrideConfigFor(LogoutAction, config =>
                {
                    config.PrimaryUrl = "logout/";
                    config.RemoveAllBehaviors();
                    config.AddBehavior<execute_the_result>();
                });

                x.OverrideConfigFor(BlogPostIndexAction, config =>
                {
                    //TODO: This stinks, there should be a way to do the "blog" part without having to deal with the URL parameters
                    config.PrimaryUrl = "blog{0}".ToFormat(x.Conventions.UrlRouteParametersForAction(config));
                });

                x.OverrideConfigFor(BlogPostCommentAction, config =>
                {
                    //TODO: This stinks, there should be a way to do the "blog" part without having to deal with the URL parameters
                    //TODO: Not sure about the placement of "/comment" here
                    config.PrimaryUrl = "blog{0}/comment".ToFormat(x.Conventions.UrlRouteParametersForAction(config));
                    config.UseViewFrom(BlogPostIndexAction);
                });

                x.OverrideConfigFor(TagIndexAction, config =>
                {
                    config.PrimaryUrl = "tag/{Tag}";
                });

                x.OverrideConfigFor(PageNotFoundIndexAction, config =>
                {
                    config.PrimaryUrl = "404";
                });

                x.OverrideConfigFor(DebugIndexAction, config =>
                {
                    config.PrimaryUrl = "__debug_controller_actions";
                });
            };

            Bootstrapper.Bootstrap();
        }

        private static HtmlExpressionBase FohjinDefaultPartialHeader(object itemList, int totalCount)
        {
            GenericOpenTagExpression expr = new GenericOpenTagExpression("ul");
            if (itemList is IEnumerable<PostDisplay>)
            {
                expr = new GenericOpenTagExpression("div");
                expr.Class("blog_post");
                return expr;
            }

            // For Debug View
            if (itemList is IEnumerable<ControllerActionDisplay>) expr.Class("controlleraction");
            if (itemList is IEnumerable<DebugSingleLineDisplay>)
            {
                expr = new GenericOpenTagExpression("ol");
                expr.Class("behavior");
            }

            return expr;
        }

        private static string FohjinDefaultPartialFooter(object itemList, int totalCount)
        {
            if (itemList is IEnumerable<PostDisplay>) return "</div>";
            return "</ul>";
        }

        private static HtmlExpressionBase FohjinDefaultPartialBeforeEachItem(object item, int index, int total)
        {
            GenericOpenTagExpression expr = new GenericOpenTagExpression("li");
            if (item is PostDisplay)
            {
                expr = new GenericOpenTagExpression("div");
                if (index == (total - 1)) expr.Class("last");
                return expr;
            }
            if (item is CommentDisplay)
            {
                expr = new GenericOpenTagExpression("div");
                expr.Class("comment-field");
                expr.Class("curvy_corner_all");
                return expr;
            }

            if (item is CommentDisplay)
            {
                if (index % 2 != 0) expr.Class("odd");

                var comment = (CommentDisplay) item;
                if (comment.User != null) expr.Class(!comment.User.IsAuthenticated ? "anon" : comment.User.ID == comment.Post.User.ID ? "author" : "user");
            }

            return expr;
        }

        private static string FohjinDefaultPartialAfterEachItem(object item, int index, int totalCount)
        {
            if (item is PostDisplay) return "</div>";
            if (item is CommentDisplay) return "</div>";
            return "</li>";
        }

        private readonly Expression<Func<HomeController, object>> HomeIndexAction = c => c.Index(null);
        private readonly Expression<Func<LoginController, object>> LogoutAction = c => c.Logout(null);
        private readonly Expression<Func<BlogPostController, object>> BlogPostIndexAction = c => c.Index(null);
        private readonly Expression<Func<BlogPostController, object>> BlogPostCommentAction = c => c.Comment(null);
        private readonly Expression<Func<TagController, object>> TagIndexAction = c => c.Index(null);
        private readonly Expression<Func<PageNotFoundController, object>> PageNotFoundIndexAction = c => c.Index(null);
        private readonly Expression<Func<DebugController, object>> DebugIndexAction = c => c.Index(null);
    }
}
