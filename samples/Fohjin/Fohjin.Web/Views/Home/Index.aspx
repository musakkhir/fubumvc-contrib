<%@ Page Inherits="HomeIndexView" MasterPageFile="~/Views/Shared/Site.Master"%>
<%@ Import Namespace="FubuMVC.Core"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<%= this.RenderPartial().Using<BlogPost>().ForEachOf(Model.Posts) %>
<div style="text-align:right; font-size:12px;">
<%= "Navigate to {0}, {1} or the {2}".ToFormat("<a href='/Home/Index'>home</a>", "<a href='/Home/Page/{0}'>« previous page</a>".ToFormat(Model.PreviousPage), "<a href='/Home/Page/{0}'>next page »</a>".ToFormat(Model.NextPage)).If(() => Model.ShowBothLinks) %>
<%= "Navigate to {0} or the {1}".ToFormat("<a href='/Home/Index'>home</a>", "<a href='/Home/Page/{0}'>« previous page</a>".ToFormat(Model.PreviousPage)).If(() => Model.ShowPreviousPageLink).IfNot(() => Model.ShowNextPageLink) %>
<%= "Navigate to {0}".ToFormat("<a href='/Home/Page/{0}'>next page »</a>".ToFormat(Model.NextPage)).If(() => Model.ShowNextPageLink).IfNot(() => Model.ShowPreviousPageLink) %>
</div>
</asp:Content>
<asp:Content ID="SidePannelContent" ContentPlaceHolderID="SidePannelContent" runat="server">
    <%= this.RenderPartial().Using<RecentBlogPosts>().For(Model.RecentPosts) %>
    <%= this.RenderPartial().Using<IsReading>().For(Model) %>    
</asp:Content>
