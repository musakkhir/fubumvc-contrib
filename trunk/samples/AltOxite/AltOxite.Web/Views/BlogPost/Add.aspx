<%@ Page Inherits="BlogPostAddView" MasterPageFile="~/Views/Shared/Admin.Master"%>
<%@ Import Namespace="AltOxite.Core.Domain"%>
<%@ Import Namespace="FubuMVC.Core"%>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadCssFiles">
<%= this.SkinCSS("jquery.css") %>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="post addPost" id="post">
    <%= this.RenderPartial().Using<BlogPostEditControl>().For(Model) %>
</div>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Scripts">
 <%= this.SkinScript(new[] { "admin.js", "jquery-ui-20081126-1.5.2.js" }).Indent("    ")%>
</asp:Content>