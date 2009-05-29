using System.Security.Principal;
using FubuMVC.Core.Security;

namespace FubuMVC.Framework.Security
{
	public class LoggedOnPrincipleFactory : IPrincipalFactory
    {
        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            return new LoggedOnPrincipal(identity);
        }
    }
}