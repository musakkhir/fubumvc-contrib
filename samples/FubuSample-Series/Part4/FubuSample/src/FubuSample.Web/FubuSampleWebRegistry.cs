using FluentNHibernate.Framework;
using FubuSample.Core.Config;
using FubuSample.Core.Domain.Persistence;
using FubuSample.Core.Persistence;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using IRepository=FubuSample.Core.Persistence.IRepository;

namespace FubuSample.Web
{
    public class FubuSampleWebRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<ISessionSourceConfiguration>().AsSingletons()
                .TheDefault.Is.OfConcreteType<SQLServerSessionSourceConfiguration>();


            ForRequestedType<ISessionSource>().AsSingletons()
                .TheDefault.Is.ConstructedBy(ctx =>
                                             ctx.GetInstance<ISessionSourceConfiguration>()
                                                 .CreateSessionSource(new FubuSamplePersistenceModel()));

            ForRequestedType<IUnitOfWork>().TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<INHibernateUnitOfWork>());

            ForRequestedType<INHibernateUnitOfWork>().CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<NHibernateUnitOfWork>();

            ForRequestedType<IRepository>().TheDefault.Is.OfConcreteType<NHibernateRepository>();
        }
    }
}