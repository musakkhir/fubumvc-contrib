using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Security;
using FubuMVC.Core.View;
using FubuMVC.Framework.Security;
using FubuMVC.Framework.Spark;
using FubuTask.Core.Domain;
using Spark;
using Spark.FileSystem;
using StructureMap.Configuration.DSL;

namespace FubuTask.Config
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            ForRequestedType<IViewFolder>().AsSingletons().TheDefault.Is.ConstructedBy(
                ctx =>
                {
                    var folder = new VirtualPathProviderViewFolder(ctx.GetInstance<FubuConventions>().ViewFileBasePath);
                    folder.AddSharedPath(ctx.GetInstance<SparkConventions>().LayoutViewFileBasePath);
                    return folder;
                });

            ForRequestedType<ISparkSettings>().TheDefault.IsThis(new SparkSettings());

            ForRequestedType<ISparkViewEngine>().TheDefault.Is.OfConcreteType<SparkViewEngine>()
                .SetterDependency(e => e.Settings).IsTheDefault()
                .SetterDependency(e => e.ViewFolder).IsTheDefault();
            


            ForRequestedType<IViewRenderer>().TheDefault.Is.OfConcreteType<SparkViewRenderer>();

            ForRequestedType<SparkConventions>().AsSingletons().TheDefault.Is.ConstructedBy(
                ctx => new SparkConventions(ctx.GetInstance<FubuConventions>()));

            ForRequestedType<IPrincipalFactory>()
                .TheDefault.Is.OfConcreteType<LoggedOnPrincipleFactory<User>>();

            //ForRequestedType<ISecurityDataService>().TheDefault.Is.OfConcreteType<SecurityDataService>();
        }
    }
}