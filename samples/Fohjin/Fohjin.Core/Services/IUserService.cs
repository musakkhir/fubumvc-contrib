using Fohjin.Core.Domain;

namespace Fohjin.Core.Services
{
    public interface IUserService 
    {
        User AddOrUpdateUser(string userEmail, string userDisplayName, string userUrl, string twitterUserName);
    }
}