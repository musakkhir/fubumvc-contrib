<%@ Control Language="C#" AutoEventWireup="true" Inherits="PostEditButtons" %>
<div class="admin buttons">
                <input type="submit" value="Save" class="button submit" tabindex="" />
                <button class="cancel" onclick="if (window.confirm('really?')){window.document.location='<%= this.UrlTo().Admin() %>';}return false;">Cancel</button>
                <a class="cancel" href="<%= this.UrlTo().Admin() %>">Cancel</a>
                <%--- Todo: Build HTML Helper for this:<%= thisHtml.Button(
                    "cancel",
                    Model.Localize("Cancel"),
                    new { @class = "cancel", onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("really?"), Model.Item != null ? Url.Post(Model.Item) : Url.Home()) }
                    )%>
                <%=Html.Link(
                    Model.Localize("Cancel"),
                    Model.Item != null ? Url.Post(Model.Item) : Url.Home(),
                    new { @class = "cancel" })%>---%>
            </div>