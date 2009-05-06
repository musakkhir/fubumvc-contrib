using FubuMVC.Core.Security;
using FubuMVC.Framework.Security;
using FubuTask.Core.Domain;
using StructureMap.Configuration.DSL;

namespace FubuTask.Config
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            ForRequestedType<IPrincipalFactory>()
                .TheDefault.Is.OfConcreteType<LoggedOnPrincipleFactory<User>>();

            //ForRequestedType<ISecurityDataService>().TheDefault.Is.OfConcreteType<SecurityDataService>();
        }
    }
}