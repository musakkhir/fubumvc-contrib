using Fohjin.Core.Domain;

namespace Fohjin.Core.Web.DisplayModels
{
    public class CommentDisplay
    {
        public CommentDisplay(Comment comment)
        {
            LocalPublishedDate = comment.Published.Value.ToString("MMMM dd, yyyy");
            PermalinkHash = comment.Published.Value.ToString("yyyyMMddhhmmssf");
            User = comment.User;
            Body = comment.Body;
            Post = comment.Post;
            Date = comment.Published.HasValue ? comment.Published.Value.ToLongDateString() : "";
            Time = comment.Published.HasValue ? comment.Published.Value.ToShortTimeString() : "";
        }

        public string Body { get; private set; }
        public User User { get; private set; }
        public Post Post { get; private set; }
        public string LocalPublishedDate { get; private set; }
        public string PermalinkHash { get; private set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}