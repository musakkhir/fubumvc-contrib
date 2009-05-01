using FluentNHibernate.Cfg.Db;

namespace Fohjin.Core.Config
{
    public class SQLServerPersistenceConfiguration : IPersistenceConfiguration
    {
        private readonly string _dbServerAddress;
        private readonly string _dbName;

        public SQLServerPersistenceConfiguration(string db_server_address, string db_name)
        {
            _dbServerAddress = db_server_address;
            _dbName = db_name;
        }

        public IPersistenceConfigurer GetConfiguration()
        {
            return MsSqlConfiguration
                    .MsSql2005
                    .ConnectionString(c =>
                    {
                        c.Server(_dbServerAddress);
                        c.Database(_dbName);
                        c.TrustedConnection();
                    })
                    .UseOuterJoin();
        }
    }
}