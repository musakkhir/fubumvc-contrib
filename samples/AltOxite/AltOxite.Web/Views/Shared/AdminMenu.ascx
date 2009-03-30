<%@ Control Language="C#" AutoEventWireup="true" Inherits="AdminMenu" %>
<ul class="menu admin">
    <li><a href="<%= this.UrlTo().Admin() %>">Admin</a></li>
    <li><a href="<%=this.UrlTo().AddPost()%>">Add Post</a></li>
    <%--- <li><%=Html.Link(Model.Localize("AddPageLinkText", "Add Page"), Url.AddPage())%></li>
    <li><%=Html.Link(Model.Localize("ManageSiteLinkText", "Manage Site"), Url.ManageSite())%></li>---%>
</ul>