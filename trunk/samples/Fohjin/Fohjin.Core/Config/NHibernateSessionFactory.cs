using NHibernate;
using FluentNHibernate.Cfg;

namespace Fohjin.Core.Config
{
    public interface INHibernateSessionFactory 
    {
        ISession CreateSession();
    }

    public class NHibernateSessionFactory : INHibernateSessionFactory
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateSessionFactory(IPersistenceConfiguration persistenceConfiguration, INHibernatePersistenceModel nHibernatePersistenceModel)
        {
            _sessionFactory = Fluently.Configure()
                .Database(persistenceConfiguration.GetConfiguration())
                .Mappings(m => m.AutoMappings.Add(nHibernatePersistenceModel.GetPersistenceModel()))
                .BuildSessionFactory();
        }

        public ISession CreateSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}