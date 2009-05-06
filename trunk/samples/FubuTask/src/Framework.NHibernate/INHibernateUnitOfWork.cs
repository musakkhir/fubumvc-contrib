using FubuMVC.Framework.Persistence;
using NHibernate;

namespace FubuMVC.Framework.NHibernate
{
    public interface INHibernateUnitOfWork : IUnitOfWork
    {
        ISession CurrentSession { get;}
    }
}