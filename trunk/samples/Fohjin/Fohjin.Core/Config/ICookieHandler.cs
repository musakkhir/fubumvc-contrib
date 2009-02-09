using System.Web;

namespace Fohjin.Core.Config
{
    public interface ICookieHandler
    {
        string UserId { get; }
        bool IsCookieThere { get; }
        ICookieHandler ForHttpRequest(HttpRequest httpRequest);
    }
}