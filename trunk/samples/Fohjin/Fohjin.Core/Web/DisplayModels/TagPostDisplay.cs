using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fohjin.Core.Domain;

namespace Fohjin.Core.Web.DisplayModels
{
    public class TagPostDisplay
    {
        public TagPostDisplay(Post post)
        {
            Published = post.Published.GetValueOrDefault(DateTime.MinValue);
            LocalPublishedDate = Published.ToLongDateString(); //TODO: To local time
            Slug = post.Slug;
            Comments = post.GetComments().OrderBy(c => c.Published).Select(c => new CommentDisplay(c));
            CommentsCount = post.GetComments().Count();
            Title = post.Title;
            Body = StripHtml(post.Body).Substring(0, 200);
            Tags = post.GetTags().OrderByDescending(t => t.CreatedDate).Select(t => new TagDisplay(t)); //TODO: this (OrderByDescending) is business logic and needs to be moved outta here most likely
            User = post.User;
            DateValue = Convert.ToInt32((Published.Day + Published.Month + Published.Hour) * Published.Minute);
        }

        const string HTML_TAG_PATTERN = "<.*?>";
        private static string StripHtml(string inputString)
        {
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        public int DateValue { get; set; }
        public DateTime Published { get; set; }
        public string LocalPublishedDate { get; set; }
        public string Slug { get; set; }
        public int CommentsCount { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public IEnumerable<CommentDisplay> Comments { get; set; } //TODO: Make this 'CommentDisplay' or something
        public IEnumerable<TagDisplay> Tags { get; set; } //TODO: Make this 'TagDisplay' or something
        public User User { get; set; } //TODO: Make this 'UserDisplay' or something
    }
}