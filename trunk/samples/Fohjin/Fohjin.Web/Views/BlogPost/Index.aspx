<%@ Page Inherits="BlogPostIndexView" MasterPageFile="~/Views/Shared/Site.Master"%>
<%@ Import Namespace="Fohjin.Core.Domain"%>
<%@ Import Namespace="Fohjin.Core.Web.DisplayModels"%>
<%@ Import Namespace="FubuMVC.Core"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="blog_post">
    <div>
        <div class="headerbox">
            <%= this.GetPuzzlePieceImage(Model.Post.DateValue) %>
	        <h1><%= Model.Post.Title %></h1>
	        <div class="subline">by <a href="<%= this.UrlTo().About() %>"><%= Model.Post.User.DisplayName %></a>, in <%= this.RenderPartial().Using<TagLink>().WithoutListWrapper().WithoutItemWrapper().ForEachOf(Model.Post.Tags) %>  |  <%= Model.Post.LocalPublishedDate %>  |  <a href="<%= this.UrlTo().PublishedPost(Model.Post) %>#comments"><%= this.GetCommentsText(Model.Post) %></a></div>
        </div>
        <div class="body">
            <%= Model.Post.Body%>
        </div>
    </div>
</div>
<div class="comment"><a name="comments"></a>
    <%= this.RenderPartial().Using<BlogPostComment>().WithoutListWrapper().WithDefault("<h2>{0}</h2>".ToFormat("No comments yet!<br /><br />")).ForEachOf(Model.Post.Comments) %>
    <%= this.DisplayDependingOnLoginStatus().For(Model.CurrentUser).UseModel(Model.Comment).WhenLoggedInShow<LoggedInCommentForm>().WhenLoggedOutShow<LoggedOutCommentForm>()%>
</div>
</asp:Content>
<asp:Content ID="SidePannelContent" ContentPlaceHolderID="SidePannelContent" runat="server">
    <%= this.RenderPartial().Using<RecentBlogPosts>().For(Model.RecentPosts) %>
    <%= this.RenderPartial().Using<IsReading>().For(Model) %>
</asp:Content>
