<%@ Control Language="C#" AutoEventWireup="true" Inherits="LoggedOutMenu" %>
<ul id="menu">
	<li><a href="<%= this.UrlTo().Home() %>">Home</a></li>
	<li><a href="<%= this.UrlTo().About() %>">About</a></li>
	<li><a href="http://feeds2.feedburner.com/Fohjin">RSS</a></li>
	<li><a href="http://fohjin.blogspot.com/">My old Blog</a></li>
	<li><a href="<%= this.UrlTo().Login() %>">Login</a></li>
</ul>