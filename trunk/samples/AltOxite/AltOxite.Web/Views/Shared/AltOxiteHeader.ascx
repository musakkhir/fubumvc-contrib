<%@ Control Language="C#" AutoEventWireup="true" Inherits="AltOxiteHeader" %>

            <div id="title">
               <h1><a href="<%= this.UrlTo().Home() %>"><%= Model.SiteName %></a></h1>
            </div>
            <div id="logindisplay">
                <%= this.DisplayDependingOnLoginStatus().For(Model.CurrentUser).WhenLoggedInShow<LoggedInStatus>().WhenLoggedOutShow<LoggedOutStatus>()%>
            </div>
            <div id="menucontainer">
                <ul class="menu">
                    <li class="home"><a href="<%= this.UrlTo().Home() %>">Home</a></li>
                 </ul>
                <!-- //TODO: Menu -->
                <%= this.DisplayDependingOnLoginStatus().For(Model.CurrentUser).WhenLoggedInShow<AdminMenu>().WhenLoggedOutShow<EmptyControl>() %>
            </div>            
