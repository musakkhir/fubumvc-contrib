using Fohjin.Core.Config;
using Fohjin.Core.Domain;
using Fohjin.Core.Domain.Persistence;
using Fohjin.Core.Persistence;
using Fohjin.Core.Security;
using Fohjin.Core.Services;
using FluentNHibernate.Framework;
using Fohjin.Core.Web.Controllers;
using Fohjin.Core.Web.FeedConvertors;
using FubuMVC.Core.Controller;
using FubuMVC.Core.Security;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using IRepository = Fohjin.Core.Persistence.IRepository;

namespace Fohjin.Web
{
    public class FohjinWebRegistry : Registry
    {
        public FohjinWebRegistry()
        {
            //ForRequestedType<ISessionSourceConfiguration>().AsSingletons()
            //    .TheDefault.Is.OfConcreteType<SQLiteSessionSourceConfiguration>()
            //    .WithCtorArg("db_file_name")
            //        .EqualToAppSetting("Fohjin.blog.sql_lite_db_file_name");

            ForRequestedType<ISessionSourceConfiguration>().AsSingletons()
                .TheDefault.Is.OfConcreteType<SQLServerSessionSourceConfiguration>()
                .WithCtorArg("db_server_address")
                    .EqualToAppSetting("fohjin.blog.db_server_address")
                .WithCtorArg("db_name")
                    .EqualToAppSetting("fohjin.blog.db_name")
                .WithCtorArg("reset_db")
                    .EqualToAppSetting("fohjin.blog.reset_db");

            ForRequestedType<ICookieHandler>().AsSingletons()
                .TheDefault.Is.OfConcreteType<CookieHandler>()
                .WithCtorArg("cookie_path")
                    .EqualToAppSetting("Fohjin.blog.cookie_path_for_user_id");

            ForRequestedType<ISessionSource>().AsSingletons()
                .TheDefault.Is.ConstructedBy(ctx => 
                    ctx.GetInstance<ISessionSourceConfiguration>()
                    .CreateSessionSource(new FohjinPersistenceModel()));

            ForRequestedType<IUnitOfWork>().TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<INHibernateUnitOfWork>());

            ForRequestedType<INHibernateUnitOfWork>().CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<NHibernateUnitOfWork>();

            ForRequestedType<IRepository>().TheDefault.Is.OfConcreteType<NHibernateRepository>();

            ForRequestedType<ISecurityDataService>().TheDefault.Is.OfConcreteType<SecurityDataService>();
            ForRequestedType<IPrincipalFactory>().TheDefault.Is.OfConcreteType<FohjinPrincipalFactory>();
            ForRequestedType<IBlogPostCommentService>().TheDefault.Is.OfConcreteType<BlogPostCommentService>();
            ForRequestedType<IUserService>().TheDefault.Is.OfConcreteType<UserService>();

            ForRequestedType<IApplicationFirstRunHandler>()
                .TheDefault.Is.OfConcreteType<DefaultApplicationFirstRunHandler>();

            ForRequestedType<SiteConfiguration>()
                .AsSingletons()
                .TheDefault.Is.ConstructedBy(() =>
                    new SiteConfiguration()
                    .FromAppSetting("Fohjin.blog.SiteConfiguration"));

            ForRequestedType<IFeedConverterFor<IndexViewModel>>()
                .TheDefault.Is.OfConcreteType<IndexViewModelFeedConvertor>();
        }
    }
}