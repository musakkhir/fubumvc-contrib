<%@ Control Language="C#" AutoEventWireup="true" Inherits="PostListSmall" %>
<li class="post">
        <div><span class="title"><a href="<%= this.UrlTo().PublishedPost(Model) %>"><%= Model.Title %></a>
            </span> <span class="comments">- <a href="<%= this.UrlTo().PublishedPost(Model) %>#comments"><%= string.Format("{0} comment{1}", Model.CommentsCount, Model.CommentsCount == 1 ? "" : "s") %></a>
            </span><a href="<%= this.UrlTo().EditPost(Model.Slug) %>">{Edit}</a></div>
        <div class="info"><span class="posted"><%= Model.Published != DateTime.MinValue ? Model.LocalPublishedDate : "Draft" %></span></div>
    </li>
    
    <!-- Todo: Need To implement multiple areas before this is usable-->
    <%---
        if (Model.Site.HasMultipleAreas)
            Response.Write(string.Format(
                Model.Localize("<span>From the {0} Blog. | </span>"),
                Html.Link(post.Area.Name.CleanText(), Url.Posts(post.Area))
                )); ---%>