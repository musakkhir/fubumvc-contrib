using Fohjin.Core.Domain;
using Fohjin.Core.Web.Html;

namespace Fohjin.Core.Web.DisplayModels
{
    public class LoginStatusDisplay
    {
        public LoginStatusDisplay(User user)
        {
            CurrentUser = user;
        }

        public User CurrentUser { get; private set; }
    }
}