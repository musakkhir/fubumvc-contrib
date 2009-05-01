using FluentNHibernate.Testing;
using Fohjin.Core.Config;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Fohjin.IntegrationTests
{
    [TestFixture]
    public class CreateDataBaseSchema
    {
        private SingleConnectionSessionSourceForSQLiteInMemoryTesting _source;

        [Test]
        [Ignore("Use_this_to_generate_the_database_schema_when_needed_by_default_it_is_ignored")]
        public void Use_this_to_generate_the_database_schema_when_needed_by_default_it_is_ignored()
        {
            var configuration = new SQLServerPersistenceConfiguration(@"RCKLIENT45\SQLEXPRESS", "Fohjin.Blog");
            var autoPersistenceModel = new NHibernatePersistenceModel();

            var config = new Configuration();
            configuration.GetConfiguration().ConfigureProperties(config);

            _source = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(config.Properties, autoPersistenceModel.GetPersistenceModel());
            _source.BuildSchema();
        }
    }
}