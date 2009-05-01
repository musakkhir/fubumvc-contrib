using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using Fohjin.Core.Config;
using Fohjin.Core.Domain;
using NUnit.Framework;

namespace Fohjin.IntegrationTests.Domain_Persistence
{
    public class PersistenceTesterContext<ENTITY>
        where ENTITY : DomainEntity, new()
    {
        private SingleConnectionSessionSourceForSQLiteInMemoryTesting _source;

        [SetUp]
        public void SetUp()
        {
            var properties = new SQLiteConfiguration()
                .UseOuterJoin()
                //.ShowSql()
                .InMemory()
                .ToProperties();

            var autoPersistenceModel = new NHibernatePersistenceModel().GetPersistenceModel();
                
            _source = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(properties, autoPersistenceModel);
            _source.BuildSchema();
        }

        public PersistenceSpecification<ENTITY> Specification { get { return new PersistenceSpecification<ENTITY>(_source); } }
    }
}