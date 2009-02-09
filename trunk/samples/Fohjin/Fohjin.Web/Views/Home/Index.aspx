<%@ Page Inherits="HomeIndexView" MasterPageFile="~/Views/Shared/Site.Master"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<%= this.RenderPartial().Using<BlogPost>().ForEachOf(Model.Posts) %>
</asp:Content>
