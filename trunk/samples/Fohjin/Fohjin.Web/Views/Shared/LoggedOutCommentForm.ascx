<%@ Import Namespace="Fohjin.Core.Web.DisplayModels"%>
<%@ Import Namespace="FubuMVC.Core"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="LoggedOutCommentForm" %>
<%@ Import Namespace="FubuMVC.Validation.Extensions"%>
<div class="comment-submit curvy_corner_all">
	<%= this.FormFor("{0}".ToFormat(this.UrlTo().CommentToPublishedPost(Model.Post))).Class("user")%>
        <fieldset>
		    <h2>Leave a Comment</h2><a name="leave_a_comment"></a>    
		    <%= this.Validate().NavigateHereWhenInvalid() %>
		    <%= this.Validate(m => m.DisplayName).DisplayWhenInvalid("<span style='color:red;'>You should provide a name if you want to leave a comment</span><br />") %>
		    <%= this.TextBoxFor(m => m.DisplayName).ElementId("comment_name").Attr("tabindex", "1").Attr("title", "Your name...")%><br />
		    <%= this.Validate(m => m.Email).DisplayWhenInvalid("<span style='color:red;'>I would like your e-mail address so I can show your Gravatar with your comment</span><br />")%>
		    <%= this.TextBoxFor(m => m.Email).ElementId("comment_email").Attr("tabindex", "2").Attr("title", "Your email (shows your gravatar)...")%><br />
		    <%= this.Validate(m => m.OptionalUrl).DisplayWhenInvalid("<span style='color:red;'>If you want to leave a website url, then make sure it is correct :)</span><br />")%>
		    <%= this.TextBoxFor(m => m.OptionalUrl).ElementId("comment_url").Attr("tabindex", "3").Attr("title", "Your website address (optional)...")%><br />
		    <%= this.Validate(m => m.Body).DisplayWhenInvalid("<span style='color:red;'>Hmmm leavinga comment without a comment, perhaps something went wrong?</span><br />")%>
		    <%= this.TextBoxFor(m => m.Body).ElementId("comment_body").MultilineMode().Attr("cols", "75").Attr("rows", "10").Attr("tabindex", "4").Attr("title", "Leave a comment...")%>
	        <label><%= this.CheckBoxFor(m => m.Remember).ElementId("comment_remember").Attr("tabindex", "5")%> Remember</label>
	        <label><%= this.CheckBoxFor(m => m.Subscribed).ElementId("comment_usersubscribed").Attr("tabindex", "6")%> Subscribe for updates to this post</label>
	        <%= this.SubmitButton("Submit", "comment_submit").ElementId("comment_submit").Attr("tabindex", "7")%>
		</fieldset>
        <script type="text/javascript">window.stringResources = { "comment_name.Your name...": "Your name...", "comment_email.Your email (shows your gravatar)...": "Your email (shows your gravatar)...", "comment_url.Your website address (optional)...": "Your website address (optional)...", "comment_body.Leave a comment...": "Leave a comment..." };</script>
	</form>
</div>