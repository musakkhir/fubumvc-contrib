using Dimebrain.TweetSharp.Fluent;
using FubuMVC.Core;

namespace Fohjin.Core.Services
{
    public interface ITwitterClient 
    {
        bool SendReply(string twitterUserName, string twitterPassword, string twitterUserNameReceiver, string message);
    }

    public class TwitterClient : ITwitterClient
    {
        public bool SendReply(string twitterUserName, string twitterPassword, string twitterUserNameReceiver, string message)
        {
            var update = FluentTwitter.CreateRequest()
                .AuthenticateAs(twitterUserName, twitterPassword)
                .Statuses()
                .Update("@{0} {1}".ToFormat(twitterUserNameReceiver, message))
                .AsJson();

            update.Request();

            return true;
        }
    }
}