<%@ Page Inherits="HomeIndexView" MasterPageFile="~/Views/Shared/Site.Master"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<%= this.RenderPartial().Using<BlogPost>().ForEachOf(Model.Posts) %>
</asp:Content>
<asp:Content ID="SidePannelContent" ContentPlaceHolderID="SidePannelContent" runat="server">
    <%= this.RenderPartial().Using<RecentBlogPosts>().For(Model.RecentPosts) %>
    <%= this.RenderPartial().Using<IsReading>().For(Model) %>
</asp:Content>
