<%@ Import Namespace="Fohjin.Core.Web.DisplayModels"%>
<%@ Import Namespace="FubuMVC.Core"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="LoggedOutCommentForm" %>
<div class="comment-submit curvy_corner_all">
	<%= this.FormFor("{0}".ToFormat(this.UrlTo().CommentToPublishedPost(Model.Post))).Class("user")%>
        <fieldset>
		    <h2>Leave a Comment</h2>
		    <%= this.TextBoxFor(m => m.User.DisplayName).ElementId("comment_name").Attr("tabindex", "1").Attr("title", "Your name...")%>
		    <%= this.TextBoxFor(m => m.User.Email).ElementId("comment_email").Attr("tabindex", "2").Attr("title", "Your email...")%>
		    <%= this.TextBoxFor(m => m.User.Url).ElementId("comment_url").Attr("tabindex", "3").Attr("title", "Your website address...")%>
		    <%= this.TextBoxFor(m => m.Body).ElementId("comment_body").MultilineMode().Attr("cols", "75").Attr("rows", "10").Attr("tabindex", "6").Attr("title", "Leave a comment...")%>
	        <label><%= this.CheckBoxFor(m => m.User.Remember).ElementId("comment_remember").Attr("tabindex", "4")%> Remember</label>
	        <label><%= this.CheckBoxFor(m => m.UserSubscribed).ElementId("comment_usersubscribed").Attr("tabindex", "6")%> Subscribe for updates to this post</label>
	        <%= this.SubmitButton("Submit", "comment_submit").ElementId("comment_submit").Attr("tabindex", "7")%>
		</fieldset>
        <script type="text/javascript">window.stringResources = { "comment_name.Your name...": "Your name...", "comment_email.Your email...": "Your email...", "comment_url.Your website address...": "Your website address...", "comment_body.Leave a comment...": "Leave a comment..." };</script>
	</form>
</div>