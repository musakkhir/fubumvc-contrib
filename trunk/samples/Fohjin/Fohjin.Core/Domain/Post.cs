using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fohjin.Core.Domain
{
    public class Post : DomainEntity
    {
        private IList<Tag> _tags = new List<Tag>();
        private  IList<Comment> _comments = new List<Comment>();

        public virtual string Title { get; set; }
        public virtual DateTime? Published { get; set; }
        public virtual string Body { get; set; }
        public virtual string Slug { get; set; }
        public virtual User User { get; set; }

        public virtual void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }
        public virtual void RemoveTag(Tag tag)
        {
            _tags.Remove(tag);
        }
        public virtual IEnumerable<Tag> GetTags()
        {
            return _tags.AsEnumerable();
        }

        public virtual void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }
        public virtual void RemoveComment(Comment comment)
        {
            _comments.Remove(comment);
        }
        public virtual IEnumerable<Comment> GetComments()
        {
            return _comments.AsEnumerable();
        }
    }

    public class PostBySlug : IDomainQuery<Post>
    {
        private string _slug;

        public PostBySlug(string slug)
        {
            Slug = slug;
        }

        public string Slug
        {
            get { return _slug; }
            set
            {
                _slug = value;
                Expression = u => u.Slug.Equals(_slug, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public Expression<Func<Post, bool>> Expression { get; private set; }
    }
}