using FluentNHibernate.Cfg.Db;

namespace Fohjin.Core.Config
{
    public class SQLitePersistenceConfiguration : IPersistenceConfiguration
    {
        private readonly string _dbFileName;

        public SQLitePersistenceConfiguration(string db_file_name)
        {
            _dbFileName = db_file_name;
        }

        public IPersistenceConfigurer GetConfiguration()
        {
            return new SQLiteConfiguration()
                    .UsingFile(_dbFileName)
                    .UseOuterJoin();
        }
    }
}