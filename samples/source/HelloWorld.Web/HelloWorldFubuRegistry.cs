using FubuMVC.Core;
using HelloWorld.Web.Controllers.Hello;

namespace HelloWorld.Web
{
    public class HelloWorldFubuRegistry : FubuRegistry
    {
        public HelloWorldFubuRegistry()
        {
            IncludeDiagnostics(true);
            Applies.ToThisAssembly();

            Actions
                .IncludeTypesNamed(x => x.EndsWith("Controller"));

            JsonOutputIf
                .WhenTheOutputModelIs<HelloWorldJsonViewModel>();

            Routes
                .IgnoreControllerFolderName()
                .IgnoreNamespaceText("HelloWorld.Web.Controllers");

            Views.TryToAttach(x => x.by_ViewModel());
        }
    }
}