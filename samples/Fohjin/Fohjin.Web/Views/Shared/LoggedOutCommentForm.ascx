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
		    <%= this.Validate(m => m.Body).DisplayWhenInvalid("<span style='color:red;'>Hmmm leaving a comment without a comment, perhaps something went wrong?</span><br />")%>
		    <%= this.TextBoxFor(m => m.Body).ElementId("comment_body").MultilineMode().Attr("cols", "75").Attr("rows", "10").Attr("tabindex", "4").Attr("title", "Leave a comment...")%>
		    <%= this.Validate(m => m.OptionalTwitterUserName).DisplayWhenInvalid("<br /><span style='color:red;'>It seems that Twitter doesn't know this username!</span><br />")%>
		    <%= this.TextBoxFor(m => m.OptionalTwitterUserName).ElementId("comment_twitterusername").Attr("tabindex", "5").Attr("title", "Your twitter username (optional, when provided you will be updated via twitter)...")%><br />
	        <label><%= this.CheckBoxFor(m => m.Remember).ElementId("comment_remember").Attr("tabindex", "6")%> Remember</label>
	        <label><%= this.CheckBoxFor(m => m.Subscribed).ElementId("comment_usersubscribed").Attr("tabindex", "7")%> Subscribe for updates to this post via e-mail (will soon be implemented)</label>
		    <%= this.HiddenFor(m => m.Question).ElementId("comment_question")%>
		    <%= this.Validate(m => m.Answer)
		        .DisplayWhenValid("<font class=\"captcha\">Please answer the following simple question to verify that you are not a pc (at least not a smart one)!</font><br />")
		        .DisplayWhenInvalid("<font class=\"captcha\"><span style='color:red;'>Please answer the following simple question to verify that you are not a pc (at least not a smart one)!</span></font><br />")
		        .ToString() %>
		    
		    <font class="captcha"><b><%= Model.Question%> = </b> </font>
		    <%= this.TextBoxFor(m => m.Answer).ElementId("comment_answer").Attr("tabindex", "8").Attr("style", "width: 20px;")%><br />
	        <%= this.SubmitButton("Submit", "comment_submit").ElementId("comment_submit").Attr("tabindex", "9")%>
		</fieldset>
        <script type="text/javascript">            window.stringResources = { "comment_name.Your name...": "Your name...", "comment_email.Your email (shows your gravatar)...": "Your email (shows your gravatar)...", "comment_url.Your website address (optional)...": "Your website address (optional)...", "comment_body.Leave a comment...": "Leave a comment...", "comment_twitterusername.Your twitter username (optional, when provided you will be updated via twitter)...": "Your twitter username (optional, when provided you will be updated via twitter)..." };</script>
	</form>
</div>