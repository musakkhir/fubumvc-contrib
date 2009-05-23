<%@ Control Language="C#" AutoEventWireup="true" Inherits="TagBlogPost" %>
<div class="headerbox">	 
    <a href="<%= this.UrlTo().PublishedPost(Model) %>"><%= this.GetPuzzlePieceImage(Model.DateValue) %></a>
	<h1><a href="<%= this.UrlTo().PublishedPost(Model) %>"><%= Model.Title %></a></h1>
	<div class="subline">by <a href="<%= this.UrlTo().About() %>"><%= Model.User.DisplayName %></a>, in <%= this.RenderPartial().Using<TagLink>().WithoutListWrapper().WithoutItemWrapper().ForEachOf(Model.Tags) %>  |  <%= Model.LocalPublishedDate %> | <a href="<%= this.UrlTo().PublishedPost(Model) %>#comments"><%= this.GetCommentsText(Model) %></a></div>
</div>	
<div class="body">
    <%= Model.Body %>
</div>
<div id="seperator"></div>