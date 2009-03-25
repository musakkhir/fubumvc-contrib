<%@ Page Inherits="HomeIndexView" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="FubuMVC.Core.Html"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <div>
         <%= this.RenderPartial().Using<ProductInfo>().ForEachOf(Model.Products) %>
    </div>
</div>
</asp:Content>