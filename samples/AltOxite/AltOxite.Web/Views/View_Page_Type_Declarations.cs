using AltOxite.Core.Web;
using AltOxite.Core.Web.Controllers;
using AltOxite.Core.Web.DisplayModels;
using AltOxite.Core.Web.WebForms;

// Master Pages
public class SiteMasterView : AltOxiteMasterPage{}
public class AdminMasterView : AltOxiteMasterPage { }
// Pages
public class HomeIndexView : AltOxitePage<IndexViewModel> { }
public class PageNotFoundIndexView : AltOxitePage<PageNotFoundViewModel> { }
public class LoginIndexView : AltOxitePage<LoginViewModel> { }
public class BlogPostIndexView : AltOxitePage<BlogPostViewModel> { }
public class BlogPostAddView : AltOxitePage<BlogPostAddViewModel> { }
public class BlogPostEditView : AltOxitePage<BlogPostAddViewModel> { }
public class BlogPostSaveView : AltOxitePage<BlogPostViewModel> { }
public class TagIndexView : AltOxitePage<TagViewModel> { }
public class AllTagsView : AltOxitePage<TagListViewModel> { }

// User Controls
public class LoggedInStatus : AltOxiteUserControl<ViewModel> { }
public class LoggedOutStatus : AltOxiteUserControl<ViewModel> { }

public class BlogPost : AltOxiteUserControl<PostDisplay> { }
public class BlogPostEditControl : AltOxiteUserControl<BlogPostAddViewModel> { }
public class PostEditButtons : AltOxiteUserControl<BlogPostAddViewModel> { }
public class PostEditPrimaryMetaData : AltOxiteUserControl<BlogPostAddViewModel> { }
public class PostEditSecondaryMetaData : AltOxiteUserControl<BlogPostAddViewModel> { }
public class BlogPostComment : AltOxiteUserControl<CommentDisplay> { }
public class LoggedInCommentForm : AltOxiteUserControl<CommentFormDisplay> { }
public class LoggedOutCommentForm : AltOxiteUserControl<CommentFormDisplay> { }

public class TagLink : AltOxiteUserControl<TagDisplay> { }

public class AltOxiteHeader : AltOxiteUserControl<ViewModel> { }
public class AltOxiteFooter : AltOxiteUserControl<ViewModel> { }

public class EmptyControl : AltOxiteUserControl<ViewModel> { }
public class AdminMenu : AltOxiteUserControl<ViewModel> { }
// For Admin Dashboard
public class AdminDashboardView : AltOxitePage<AdminDataViewModel> { }

public class PostListSmall : AltOxiteUserControl<PostDisplay> { }

// For Debug View
public class DebugIndexView : AltOxitePage<DebugViewModel> { }
public class ControllerAction : AltOxiteUserControl<ControllerActionDisplay> { }
public class DebugSingleLine : AltOxiteUserControl<DebugSingleLineDisplay> { }
