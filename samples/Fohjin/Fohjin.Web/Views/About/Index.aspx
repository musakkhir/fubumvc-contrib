<%@ Page Inherits="AboutIndexView" MasterPageFile="~/Views/Shared/Site.Master"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="blog_post">
    <div>
        <div class="headerbox">
            <img alt="Mark Nijhof" class="left" src="http://www.gravatar.com/avatar/01d418308faffa0d07f34ace68b686ad?s=60" />
	        <h1>Mark Nijhof</h1>
	        <div class="subline">E-mail: <a href="mailto:Mark.Nijhof@Gmail.com">Mark.Nijhof@Gmail.com</a> | Phone: 0047 95 00 99 37 | Location: Bergen, Norway</div>
        </div>
        <div class="body">
            <div class="right">
                <a href="http://twitter.com/MarkNijhof" target="_blank"><img alt="Twitter" src="/content/images/twitter.png" /></a><br />
                <a href="http://feeds2.feedburner.com/Fohjin" target="_blank"><img alt="Twitter" src="/content/images/RSS.png" /></a><br />
                <a href="/Tag/NDA"><img alt="Twitter" src="/content/images/NDA.png" /></a><br />
            </div>
	        This blog is presenting my opinions about what proper Software Architecture is, and also what you can do to improve yourself and others. And of course the different technologies that I use on a daily basis will be discussed, especially when I am having trouble with it. If you want you can contact me at <a href="mailto:Mark.Nijhof@Gmail.com">Mark.Nijhof@Gmail.com</a> and you’ll also find me at <a href="http://twitter.com/MarkNijhof" target="_blank">Twitter</a>. If you want to automatically receive notification when I post a new blog post then you can register my <a href="http://feeds2.feedburner.com/Fohjin" target="_blank">RSS feed</a> in your favorite RSS Reader.
	        <br /><br />
	        A better explanation for the existence of this blog will come later.
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="SidePannelContent" ContentPlaceHolderID="SidePannelContent" runat="server">
    <%= this.RenderPartial().Using<RecentBlogPosts>().For(Model.RecentPosts) %>
    <%= this.RenderPartial().Using<IsReading>().For(Model) %>
</asp:Content>
