using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fohjin.Core.Domain
{
    public class User : DomainEntity
    {
        private IList<Post> _posts;

        public virtual string Username { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string HashedEmail { get; set; }
        public virtual string Email { get; set; }
        public virtual string Url { get; set; }
        public virtual string Password { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual int Status { get; set; }
        public virtual bool Remember { get; set; }
        public virtual bool IsAuthenticated { get; set; }
        public virtual UserRoles UserRole { get; set; }
        public virtual string TwitterUserName { get; set; }

        public virtual void AddPost(Post post)
        {
            _posts.Add(post);
        }
        public virtual void RemovePost(Post post)
        {
            _posts.Remove(post);
        }
        public virtual IEnumerable<Post> GetPosts()
        {
            return _posts.AsEnumerable();
        }
    }

    public class UserByEmail : IDomainQuery<User>
    {
        private string _email;

        public UserByEmail(string email)
        {
            Email = email;
        }

        public string Email
        {
            get { return _email; }
            set 
            { 
                _email = value;
                Expression = u => u.Email.Equals(_email, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public Expression<Func<User, bool>> Expression { get; private set; }
    }

    public enum UserRoles
    {
        SiteUser,
        Visitor,
        NotAuthenticated,
    }
}