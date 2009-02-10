<%@ Control Language="C#" AutoEventWireup="true" Inherits="RecentBlogPosts" %>
<div class="sidepanel">
	<div class="sidepanel_header"><h2>Recent blog posts</h2></div>
	<div class="sidepanel_body">
	    <%= this.RenderPartial().Using<BlogPostLink>().ForEachOf(Model.GetPosts()) %>
	</div>
	<div class="roundbottom">
        <img src="/content/images/bl.gif" alt="" width="5" height="5" class="corner" style="display: none" />
	 </div>	
</div>
