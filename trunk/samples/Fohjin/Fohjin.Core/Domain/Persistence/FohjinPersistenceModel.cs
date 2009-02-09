using FluentNHibernate;

namespace Fohjin.Core.Domain.Persistence
{
    public class FohjinPersistenceModel : PersistenceModel
    {
        public FohjinPersistenceModel()
        {
            Conventions.DefaultStringLength = 250;
            addMappingsFromThisAssembly();
        }
    }
}