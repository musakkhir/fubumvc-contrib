<%@ Control Language="C#" AutoEventWireup="true" Inherits="BlogPostComment" %>
<%= this.GetCommentPremalinkBookmark(Model) %>
<div class="comment-field curvy_corner_all">
	<div class="avatar">
	    <%= this.GetCommenterGravatarAndLink(Model) %>
	</div>
	<div class="comment-body">
		<p><%= Model.Body %></p>
		<p class=""><b><%= this.GetCommenterNameAndLink(Model) %></b>, <%= Model.Date %> at <%= Model.Time %></p>
	</div>
</div>