using NHibernate;

namespace Fohjin.Core.Persistence
{
    public interface INHibernateUnitOfWork : IUnitOfWork
    {
        ISession CurrentSession { get;}
    }
}