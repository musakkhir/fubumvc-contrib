using System;
using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FubuMVC.Framework.NHibernate;
using FubuMVC.Framework.NHibernate.Config.GuidEntity;
using FubuMVC.Framework.Persistence;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using Entity=FubuTask.Core.Domain.Entity;

namespace FubuTask.Config
{
    public class PersistenceRegistry: Registry
    {
        public PersistenceRegistry()
        {
            var mssqlConfig = MsSqlConfiguration
                .MsSql2005
                .ConnectionString(c => c.FromConnectionStringWithKey("FubuTaskDB"))
                .UseOuterJoin();

            var source = new SessionSource(
                Fluently.Configure()
                    .Database(mssqlConfig)
                    .Mappings(c => c.AutoMappings.Add(new GuidEntityPersistenceModel<Entity>()))
                );

            ForRequestedType<ISessionSource>().AsSingletons()
                .TheDefault.IsThis(source);

            ForRequestedType<IUnitOfWork>()
                .TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<INHibernateUnitOfWork>());

            ForRequestedType<INHibernateUnitOfWork>().CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.OfConcreteType<NHibernateUnitOfWork>();

            ForRequestedType<IRepository<Guid>>().TheDefault.Is.OfConcreteType<NHibernateRepository<Guid>>();
        }
    }
}