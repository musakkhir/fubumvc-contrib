<%@ Control Language="C#" AutoEventWireup="true" Inherits="BlogPostLink" %>
<a href="<%= this.UrlTo().PublishedPost(Model.Post) %>"><%= Model.Post.Title %></a>