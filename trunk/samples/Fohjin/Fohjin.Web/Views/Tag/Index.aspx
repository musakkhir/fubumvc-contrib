<%@ Page Inherits="TagIndexView" MasterPageFile="~/Views/Shared/Site.Master"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="blog_post">
    <div>
        <div class="headerbox">
            <%= this.GetPuzzlePieceImage(-1) %>
	        <h1>Tag - <%= Model.Tag.Name %></h1>
        </div>
    </div>
</div>
</asp:Content>
