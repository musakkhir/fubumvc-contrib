using System;
using Fohjin.Core.Domain;
using FubuMVC.Validation.Results;

namespace Fohjin.Core.Web.DisplayModels
{
    public class CommentFormDisplay : ICanBeValidated
    {
        public CommentFormDisplay(Comment comment, PostDisplay postDisplay)
        {
            Post = postDisplay;
            DisplayName = comment.User.DisplayName;
            Email = comment.User.Email;
            OptionalUrl = comment.User.Url;
            Subscribed = comment.UserSubscribed;
            Remember = comment.User.Remember;
            Body = comment.Body;
        }

        public PostDisplay Post { get; private set; }

        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public string OptionalUrl { get; private set; }
        public bool Remember { get; private set; }
        public bool Subscribed { get; private set; }
        public string Body { get; private set; }

        private readonly IValidationResults _validationResults = new ValidationResults();
        public IValidationResults ValidationResults
        {
            get { return _validationResults; }
        }
    }
}