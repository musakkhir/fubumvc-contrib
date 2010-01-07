<%@ Page Language="C#" MasterPageFile="~/Shared/Main.Master" AutoEventWireup="true" CodeBehind="Hello.aspx.cs" Inherits="HelloWorld.Web.Controllers.Hello.Hello" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%= Model.Text %>
</asp:Content>