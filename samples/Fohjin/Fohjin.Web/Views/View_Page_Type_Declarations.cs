using Fohjin.Core.Web;
using Fohjin.Core.Web.Controllers;
using Fohjin.Core.Web.DisplayModels;
using Fohjin.Core.Web.WebForms;

// Master Pages
public class SiteMasterView : FohjinMasterPage{}

// Pages
public class HomeIndexView : FohjinPage<IndexViewModel> { }
public class AboutIndexView : FohjinPage<AboutViewModel> { }
public class PageNotFoundIndexView : FohjinPage<PageNotFoundViewModel> { }
public class LoginIndexView : FohjinPage<LoginViewModel> { }
public class BlogPostIndexView : FohjinPage<BlogPostViewModel> { }
public class TagIndexView : FohjinPage<TagViewModel> { }

// User Controls
public class LoggedInMenu : FohjinUserControl<ViewModel> { }
public class LoggedOutMenu : FohjinUserControl<ViewModel> { }

public class BlogPost : FohjinUserControl<PostDisplay> { }
public class RecentBlogPosts : FohjinUserControl<RecentPostsDisplay> { }
public class IsReading : FohjinUserControl<ViewModel> { }
public class BlogPostLink : FohjinUserControl<BlogPostLinkDisplay> { }
public class BlogPostComment : FohjinUserControl<CommentDisplay> { }
public class LoggedInCommentForm : FohjinUserControl<CommentFormDisplay> { }
public class LoggedOutCommentForm : FohjinUserControl<CommentFormDisplay> { }

public class TagLink : FohjinUserControl<TagDisplay> { }

// For Debug View
public class DebugIndexView : FohjinPage<DebugViewModel> { }
public class ControllerAction : FohjinUserControl<ControllerActionDisplay> { }
public class DebugSingleLine : FohjinUserControl<DebugSingleLineDisplay> { }
