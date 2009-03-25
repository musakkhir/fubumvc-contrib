using FluentNHibernate;
using FluentNHibernate.Framework;

namespace FubuSample.Core.Config
{
    public interface ISessionSourceConfiguration
    {
        bool IsNewDatabase { get; }
        ISessionSource CreateSessionSource(PersistenceModel model);
    }
}