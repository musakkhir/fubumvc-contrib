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
            Mark is a Software Developer experienced in the Microsoft .Net framework; he has developed/implemented a diverse range of Object-Oriented software solutions. Mark has been working closely with customers to achieve high quality solutions. Mark is always learning new technologies en theories to improve his programming skills; he is especially interested in all the different Object-Oriented design principles like: SRP, OCP, LSP, ISP and DIP. In his free time he contributes to an OSS project, and is a board member of NNUG.
            <br /><br />
            He started his career with CSC Computer Sciences BV working on different web based applications. In April 2004 he moved to Norway and started working for Vizrt joining an existing development team in product development of one of Vizrt’s product groups. Mark moved on March 2006 to Sydney Australia to start working for Vizrt as a Solution Architect and support person. In July 2007 Mark started working for his first employer again joining the software factory in Agile SOA development. Then in June 2008 he moved back to Bergen and started working for Reaktor Consulting as an Architect.
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="SidePannelContent" ContentPlaceHolderID="SidePannelContent" runat="server">
    <%= this.RenderPartial().Using<RecentBlogPosts>().For(Model.RecentPosts) %>
    <%= this.RenderPartial().Using<IsReading>().For(Model) %>
</asp:Content>
