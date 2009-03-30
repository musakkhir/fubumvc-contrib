<%@ Control Language="C#" AutoEventWireup="true" Inherits="BlogPostEditControl" %>
<%= this.FormFor<BlogPostController>(l => l.Save(null)) %>
<h2 class="title">
	        <label for="post_title">Title</label>
	        <%= this.TextBoxFor(m => m.Title).Class("text").ElementId("post_title").WithHint("Enter Title...") %>
            <%--- Not really sure what this is RK <%=Html.OxiteAntiForgeryToken(m => m.AntiForgeryToken) %>---%>
	    </h2>
	    <%= this.RenderPartial().Using<PostEditPrimaryMetaData>().For(Model) %>
	    <%--- Todo: Implement this <% Html.RenderPartial("ItemEditPrimaryMetadata"); ---%>
	    <div class="content">
	        <label for="post_bodyShort">Excerpt</label>
	        <%= this.TextBoxFor(m => m.BodyShort).ElementId("post_bodyShort").MultilineMode().Attr("rows", "6").Attr("cols", "120").WithHint("Enter Excerpt...") %>
	        <label for="post_body">Body Content</label>
	        <%= this.TextBoxFor(m => m.Body).ElementId("post_body").Class("hinted").MultilineMode().Attr("rows", "24").Attr("cols", "120").WithHint("Enter body content")%>
	    </div>
	    <%= this.RenderPartial().Using<PostEditSecondaryMetaData>().For(Model) %>
	    <%= this.HiddenFor(m => m.Id) %>
</form>