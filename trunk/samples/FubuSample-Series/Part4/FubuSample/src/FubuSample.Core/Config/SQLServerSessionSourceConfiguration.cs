using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Framework;

namespace FubuSample.Core.Config
{
    public class SQLServerSessionSourceConfiguration : ISessionSourceConfiguration
    {
        #region Implementation of ISessionSourceConfiguration

        public bool IsNewDatabase
        {
            get { return true; }
        }

        public ISessionSource CreateSessionSource(PersistenceModel model)
        {
            var properties = GetProperties();
            var source = new SessionSource(properties, model);

            create_schema_if_it_does_not_already_exist(source);

            return source;
        }

        private void create_schema_if_it_does_not_already_exist(ISessionSource source)
        {
            if (IsNewDatabase) source.BuildSchema();
        }

        protected IDictionary<string, string> GetProperties()
        {
            MsSqlConfiguration config = MsSqlConfiguration.MsSql2005;
            config.ConnectionString.FromConnectionStringWithKey("MYDBKEY");
            config.ShowSql();
            config.UseOuterJoin();

            return config.ToProperties();
        }
        #endregion
    }
}