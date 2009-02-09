using System.Security.Principal;
using FubuMVC.Core.Security;

namespace Fohjin.Core.Security
{
    public class FohjinPrincipalFactory : IPrincipalFactory
    {
        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            return new FohjinPrincipal(identity);
        }
    }
}