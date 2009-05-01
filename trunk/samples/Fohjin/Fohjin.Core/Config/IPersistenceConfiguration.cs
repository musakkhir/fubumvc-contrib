using FluentNHibernate.Cfg.Db;

namespace Fohjin.Core.Config
{
    public interface IPersistenceConfiguration
    {
        IPersistenceConfigurer GetConfiguration();
    }
}