using NHibernate;

namespace FubuSample.Core.Persistence
{
    public interface INHibernateUnitOfWork : IUnitOfWork
    {
        ISession CurrentSession { get; }
    }
}