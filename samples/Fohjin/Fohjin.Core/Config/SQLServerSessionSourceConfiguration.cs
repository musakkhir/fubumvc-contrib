using System.Collections.Generic;
using FluentNHibernate.Cfg.Db;

namespace Fohjin.Core.Config
{
    public class SQLServerSessionSourceConfiguration : ServerBasedSessionSourceConfiguration
    {
        public SQLServerSessionSourceConfiguration(string db_server_address, string db_name, bool reset_db)
            : base(db_server_address, db_name, reset_db)
        {
        }

        protected override IDictionary<string, string> GetProperties(string db_server_address, string db_name)
        {
            return MsSqlConfiguration
                    .MsSql2005
                    .ConnectionString(c =>
                    {
                        c.Server(db_server_address);
                        c.Database(db_name);
                        c.TrustedConnection();
                    })
                    .UseOuterJoin()
                    .ToProperties();
        }
    }
}