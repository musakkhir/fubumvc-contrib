using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Fohjin.Core.Web;
using Fohjin.Core.Web.Behaviors;
using Fohjin.Core.Web.Controllers;
using Fohjin.Core.Web.DisplayModels;
using FubuMVC.Container.StructureMap.Config;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Conventions.ControllerActions;
using FubuMVC.Core.Html.Expressions;
using FubuMVC.Validation;
using FubuMVC.Validation.Behaviors;
using FubuMVC.Validation.Dsl;
using FubuMVC.Validation.Rules;

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

                    conventions.DefaultPathToPartialView = (parentView, partialViewType) =>
                    {
                        //var controllerName = conventions.CanonicalControllerName(parentView.GetType());
                        //var path = "{0}/{1}/{2}.ascx".ToFormat(conventions.ViewFileBasePath, controllerName, partialViewType.Name);
                        //return File.Exists(Path.Combine(Request.ApplicationPath, VirtualPathUtility.ToAbsolute(path)))
                        //    ? path
                        //    : "{0}/{1}.ascx".ToFormat(conventions.SharedViewFileBasePath, partialViewType.Name);
                        return "{0}/{1}.ascx".ToFormat(conventions.SharedViewFileBasePath, partialViewType.Name);
                    };
                });

                x.ActionConventions(custom =>
                {
                    custom.Add<wire_up_JSON_URL_if_required>();
                    custom.Add<wire_up_RSS_and_ATOM_URLs_if_required>();
                    custom.Add<wire_up_404_handler_URL>();
                    custom.Add<wire_up_debug_handler_URL>();
                });

                // Default Behaviors for all actions -- ordered as they're executed
                /////////////////////////////////////////////////
                x.ByDefault.EveryControllerAction(d => d
                    .Will<access_the_database_through_a_unit_of_work>()
                    //.Will<set_up_default_data_the_first_time_this_app_is_run>()

                    .Will<set_empty_default_user_on_the_output_viewmodel_to_make_sure_one_exists>()
                    .Will<load_the_current_principal>()
                    .Will<set_the_current_logged_in_user_on_the_output_viewmodel>()
                    .Will<set_user_from_http_cookie_if_current_user_is_not_authenticated>()

                    .Will<get_recent_blog_posts>()
                    .Will<validate_input_view_model_using_convention_based_validation_rules>()

                    .Will<execute_the_result>()
                    .Will<OutputAsRssOrAtomFeed>()
                    .Will<set_the_current_site_details_on_the_output_viewmodel>()
                    .Will<copy_viewmodel_from_input_to_output<ViewModel>>()
                    );
                
                // Automatic controller registration
                /////////////////////////////////////////////////
                //x.AddControllerActions(a => 
                //    a.UsingTypesInTheSameAssemblyAs<ViewModel>(t => 
                //        t.SelectTypes(type => 
                //             type.Namespace.EndsWith("Web.Controllers") && 
                //             type.Name.EndsWith("Controller"))
                //         .SelectMethods(method => 
                //             method.Name.EndsWith("Action"))));

                const BindingFlags publicDeclaredOnly = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
                x.AddControllerActions(a => 
                    a.UsingTypesInTheSameAssemblyAs<ViewModel>(types =>
                        from type in types 
                        where type.Namespace.EndsWith("Web.Controllers") && 
                              type.Name.EndsWith("Controller")
                        from method in type.GetMethods(publicDeclaredOnly)
                        select method
                ));

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

                x.OverrideConfigFor(RedirectToOldBlogIndexAction, config =>
                {
                    config.AddOtherUrl("2008/12/hire-funny-guy.html");
                    config.AddOtherUrl("2008/11/linq-is-cool-but.html");
                    config.AddOtherUrl("2008/11/being-specific-with-your-generics_26.html");
                    config.AddOtherUrl("2008/11/nnug-tfs-with-terje-sandstrm.html");
                    config.AddOtherUrl("2008/11/announcing-isolator-for-sharepoint-unit.html");
                    config.AddOtherUrl("2008/10/arnon-rotam-gal-oz-talks-about.html");
                    config.AddOtherUrl("2008/10/what-tools-i-use-when-developing.html");
                    config.AddOtherUrl("2008/10/cracknet-by-josh-smith.html");
                    config.AddOtherUrl("2008/10/james-kovacs-talks-about-roll-your-own.html");
                    config.AddOtherUrl("2008/10/my-todo-list.html");
                    config.AddOtherUrl("2008/10/new-design-of-my-blog.html");
                    config.AddOtherUrl("2008/09/is-nda-in-conflict-with-yagni.html");
                    config.AddOtherUrl("2008/09/no-dependency-architecture-nda.html");
                    config.AddOtherUrl("2008/09/silverlight-with-einar-ingebrigtsen.html");
                    config.AddOtherUrl("2008/09/that-agile-thing.html");
                    config.AddOtherUrl("2008/09/how-to-test-your-xaml-behavior-using.html");
                    config.AddOtherUrl("2008/09/how-to-reference-dlls-in-your-net.html");
                    config.AddOtherUrl("2008/09/msdn-live-bergen.html");
                    config.AddOtherUrl("2008/09/google-chrome.html");
                    config.AddOtherUrl("2008/08/ddd-and-bdd-with-dan-north-of.html");
                    config.AddOtherUrl("2008/07/saving-wpf-xaml-flowdocument-to-xps.html");
                    config.AddOtherUrl("2008/04/composite-pattern.html");
                    config.AddOtherUrl("2008/04/specification-pattern-using-springnet.html");
                    config.AddOtherUrl("2008/04/specification-pattern.html");
                    config.AddOtherUrl("2008/04/fluent-interfaces.html");
                    config.AddOtherUrl("2008/04/adapter-pattern.html");
                    config.AddOtherUrl("2008/04/strategy-pattern.html");
                    config.AddOtherUrl("2008/04/decorator-pattern.html");
                    config.AddOtherUrl("2008/04/lod-law-of-demeter.html");
                    config.AddOtherUrl("2008/04/dip-dependency-inversion-principle_21.html");
                    config.AddOtherUrl("2008/04/dip-dependency-inversion-principle.html");
                    config.AddOtherUrl("2008/04/isp-interface-segregation-principle.html");
                    config.AddOtherUrl("2008/04/lsp-liskov-substitution-principle.html");
                    config.AddOtherUrl("2008/04/ocp-open-closed-principle.html");
                    config.AddOtherUrl("2008/04/srp-single-responsibility-principle.html");
                    config.AddOtherUrl("2008/04/solid-object-orientated-design.html");
                    config.AddOtherUrl("2008/04/syntax-highlighting-in-blogger.html");
                    config.AddOtherUrl("2008/04/google-custom-search-engine-cse-for.html");
                    config.AddOtherUrl("2008/04/i-have-been-reading-about-design-by_12.html");
                });
            };

            ValidationConfig.Configure = x =>
            {
                x.ByDefault.PropertiesMatching(property => !property.Name.StartsWith("Optional"), rule => 
                    rule.WillBeValidatedBy<IsRequired<CanBeAnyViewModel>>());

                x.ByDefault.PropertiesMatching(property => property.Name.Contains("Email"), rule =>
                    rule.WillBeValidatedBy<IsEmail<CanBeAnyViewModel>>());

                x.ByDefault.PropertiesMatching(property => property.Name.Contains("Url"), rule =>
                    rule.WillBeValidatedBy<IsUrl<CanBeAnyViewModel>>());

                x.ByDefault.PropertiesMatching(property => property.Name.Contains("Answer"), rule =>
                    rule.WillBeValidatedBy<IsValidCaptcha<CanBeAnyViewModel>>(needs =>
                        needs.NeedsAdditionalProperty(property => property.Name.Contains("Question"))));

                x.ByDefault.PropertiesMatching(property => property.Name.Contains("TwitterUser"), rule =>
                    rule.WillBeValidatedBy<IsValidTwitterUser<CanBeAnyViewModel>>());

                x.AddViewModelsFromAssembly
                    .ContainingType<ViewModel>()
                    .Where(t => t.Namespace.EndsWith("Web.Controllers"));
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

        private readonly Expression<Func<LoginController, object>> LogoutAction = c => c.Logout(null);
        private readonly Expression<Func<BlogPostController, object>> BlogPostIndexAction = c => c.Index(null);
        private readonly Expression<Func<BlogPostController, object>> BlogPostCommentAction = c => c.Comment(null);
        private readonly Expression<Func<TagController, object>> TagIndexAction = c => c.Index(null);
        private readonly Expression<Func<RedirectToOldBlogController, object>> RedirectToOldBlogIndexAction = c => c.Index(null);
    }
}
