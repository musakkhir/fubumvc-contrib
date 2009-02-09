using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Framework;

namespace Fohjin.Core.Config
{
    public abstract class ServerBasedSessionSourceConfiguration : ISessionSourceConfiguration
    {
        private readonly string _serverAddress;
        private readonly string _databaseName;

        protected ServerBasedSessionSourceConfiguration(string db_server_address, string db_name, bool reset_db)
        {
            _serverAddress = db_server_address;
            _databaseName = db_name;
            IsNewDatabase = reset_db;
        }

        protected abstract IDictionary<string, string> GetProperties(string db_server_address, string db_name);

        public bool IsNewDatabase { get; private set; }

        public ISessionSource CreateSessionSource(PersistenceModel model)
        {
            var properties = GetProperties(_serverAddress, _databaseName);

            var source = new SessionSource(properties, model);

            create_schema_if_it_does_not_already_exist(source);

            return source;
        }

        private void create_schema_if_it_does_not_already_exist(ISessionSource source)
        {
            if (IsNewDatabase) source.BuildSchema();
        }
    }
}