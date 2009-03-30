<%@ Control Language="C#" AutoEventWireup="true" Inherits="PostEditSecondaryMetaData" %>
<div class="admin metadata">
    <ul>
        <li class="input slug">
            <label for="post_slug">Slug</label>
            <%= this.TextBoxFor(m => m.Slug).ElementId("post_slug").Class("text").WithHint("Enter slug...") %>
        </li>
        <!-- Todo: Need to check if the post is really a draft or not, so we can select the appropriate radio button -->
        <li class="input draft">
            <input id="post_stateDraft" type="radio" value="False" name="isPublished" checked="checked" />
            <label for="post_stateDraft" class="radio">Draft</label>
        </li>
        <li class="input publish">
            <fieldset>
                <legend>Publish</legend>
                <input id="post_statePublished" type="radio" value="True" name="isPublished" />
                <label for="post_statePublished" class="radio">Published</label>
                <label for="post_published">Publish Date</label>
                <%= this.TextBoxFor(m => m.Published).ElementId("post_published").Class("text date").WithHint("Enter publish date...") %>                
            </fieldset>
        </li>
</ul>
<%= this.RenderPartial().Using<PostEditButtons>().For(Model) %>
</div>

<%--- Original OxiteVersion:
<%
    bool urlIsLocked = Model.Item.State == EntityState.Normal
        && Model.Item.Published.HasValue
        && Model.Item.Published.Value.AddHours(Model.Site.PostEditTimeout) < DateTime.Now.ToUniversalTime();
 %>        <div class="admin metadata">
            <ul>
                <li class="input slug"><% 
                    if (ViewData.ModelState.ContainsKey("Post.Slug")) {
                        %><%= Html.ValidationMessage("Post.Slug", Model.Localize("Slug isn't valid."))%><% 
                    } else { 
                        %><label for="post_slug"><%=Model.Localize("Slug") %></label><% 
                    } 
                    %> <%=Html.TextBox(
                           "slug",
                            Request["slug"] ?? (Model.Item != null ? Model.Item.Slug : ""),
                            new { id = "post_slug", @class = "text", size = "72", title = Model.Localize("post_slug", "Enter slug...") },
                            !urlIsLocked
                            ) %></li>
                <%  if (!urlIsLocked) {
                      %><li class="input draft"><%=
                        Html.RadioButton(
                            "isPublished",
                            false,
                            !(Model.Item != null && Model.Item.Published.HasValue) || Request.Form.IsTrue("isPublished"),
                            new { id = "post_stateDraft" }
                            ) %> <label for="post_stateDraft" class="radio"><%=Model.Localize("Draft") %></label></li><%
                    } %>
                <li class="input publish">
                    <fieldset>
                        <legend><%=Model.Localize("Publish") %></legend>
                        <%=Html.RadioButton("isPublished",
                                true,
                                (Model.Item != null && Model.Item.Published.HasValue) || Request.Form.IsTrue("isPublished"),
                                new { id = "post_statePublished" },
                                !urlIsLocked
                                )%> <label for="post_statePublished" class="radio"><%=Model.Localize("post_statePublished", string.Format("Publish{0}", Model.Item != null && Model.Item.Published.HasValue && Html.ConvertToLocalTime(Model.Item.Published.Value, Model) < DateTime.Now ? "ed" : ""))%></label>
                        <label for="post_published"><%=Model.Localize("Publish Date") %></label><%= Html.ValidationMessage("Post.Published") %>
                        <%=Html.TextBox(
                            "published",
                            Model.Item != null && Model.Item.Published.HasValue && Model.Item.Published.Value > default(DateTime) ? Model.Item.Published.Value.ToStringForEdit() : "",
                            new { id = "post_published", @class = "text date", size="22", title = Model.Localize("published", "Enter publish date...") },
                            !urlIsLocked
                            ) %>
                   </fieldset>
                </li>
                <li class="input allowComments">
                    <%=Html.CheckBox("commentingDisabled", m => m.Item.CommentingDisabled, null, new { @class = "checkbox" })%> <label for="commentingDisabled" class="checkbox"><%=Model.Localize("CommentingDisabled", "Commenting Disabled") %></label>
                </li>
            </ul>
            <% Html.RenderPartial("ItemEditButtons"); %>
        </div>

 ---%>