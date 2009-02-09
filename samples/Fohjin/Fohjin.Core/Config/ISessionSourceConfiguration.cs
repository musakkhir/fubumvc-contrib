using FluentNHibernate;
using FluentNHibernate.Framework;

namespace Fohjin.Core.Config
{
    public interface ISessionSourceConfiguration
    {
        bool IsNewDatabase { get; }
        ISessionSource CreateSessionSource(PersistenceModel model);
    }
}